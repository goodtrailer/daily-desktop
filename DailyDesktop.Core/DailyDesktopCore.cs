// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;

namespace DailyDesktop.Core
{
    /// <summary>
    /// Core of Daily Desktop that handles the wallpaper update <see cref="Task"/>
    /// using Windows Task Scheduler. Also handles <see cref="IProvider"/> DLL
    /// module scanning, though <see cref="ProviderStore"/> is fully functional as
    /// a standalone class.
    /// </summary>
    public class DailyDesktopCore
    {
        //---------------------------------------------------------------VARIABLES

        private const string SETTINGS_JSON_FILENAME = "settings.json";
        private const string WALLPAPER_INFO_JSON_FILENAME = "wallpaper.json";
        private const string TASK_EXECUTABLE_FILENAME = "DailyDesktop.Task.exe";

        private readonly ProviderStore store;

        private Task task;
        private string taskName;
        private string providersDirectory;
        private string serializeJsonDirectory;
        private IProvider currentProvider;
        private DailyDesktopSettings settings;

        //--------------------------------------------------------------PROPERTIES

        /// <summary>
        /// Gets or sets whether or not to automatically create the task on
        /// when settings are changed or loaded.
        /// </summary>
        public bool AutoCreateTask { get; set; }

        /// <summary>
        /// Gets the path of the settings JSON file, based on
        /// <see cref="SerializeJsonDirectory"/>.
        /// </summary>
        public string SettingsJsonPath => Path.Combine(serializeJsonDirectory, SETTINGS_JSON_FILENAME);

        /// <summary>
        /// Gets the path of the wallpaper information JSON file, based on
        /// <see cref="SerializeJsonDirectory"/>.
        /// </summary>
        public string WallpaperInfoJsonPath => Path.Combine(serializeJsonDirectory, WALLPAPER_INFO_JSON_FILENAME);

        /// <summary>
        /// Gets or sets name of the task registered in the Windows Task
        /// Scheduler.
        /// </summary>
        public string TaskName
        {
            get => taskName;
            set
            {
                taskName = value;
                DeleteTask();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets the providers directory where <see cref="IProvider"/> DLL
        /// modules are automatically scanned from.
        /// </summary>
        public string ProvidersDirectory
        {
            get => providersDirectory;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ProvidersDirectory cannot be null.");
                Directory.CreateDirectory(value);
                providersDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets the directory where JSON serializations are saved to and
        /// read from.
        /// </summary>
        public string SerializeJsonDirectory
        {
            get => serializeJsonDirectory;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SerializeJsonDirectory cannot be null.");
                Directory.CreateDirectory(value);
                serializeJsonDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets whether wallpaper update <see cref="Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        public bool Enabled
        {
            get => settings.Enabled;
            set
            {
                settings.Enabled = value;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets whether or not to apply resize to wallpaper images to screen resolution, if larger.
        /// </summary>
        public bool DoResize
        {
            get => settings.DoResize;
            set
            {
                settings.DoResize = value;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        public bool DoBlurredFit
        {
            get => settings.DoBlurredFit;
            set
            {
                settings.DoBlurredFit = value;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets the blur strength for wallpaper images. Only applies if
        /// <see cref="DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        public int BlurStrength
        {
            get => settings.BlurStrength;
            set
            {
                settings.BlurStrength = value;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets the current provider to fetch wallpaper image URIs from
        /// in a <see cref="ProviderWrapper"/>.
        /// </summary>
        public ProviderWrapper? CurrentProvider
        {
            get
            {
                if (string.IsNullOrWhiteSpace(settings.DllPath) || currentProvider == null)
                    return null;
                else
                    return new ProviderWrapper(settings.DllPath, currentProvider);
            }
            set
            {
                currentProvider = value?.Provider;
                settings.DllPath = value?.DllPath;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets the time at which the daily wallpaper update trigger
        /// executes. Only applies if <see cref="Enabled"/> is set to <c>true</c>.
        /// </summary>
        public DateTime UpdateTime
        {
            get => settings.UpdateTime;
            set
            {
                settings.UpdateTime = value;
                SaveSettings();
                if (AutoCreateTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets a freshly scanned dictionary of <see cref="IProvider"/>
        /// <see cref="Type"/> values and DLL module path keys.
        /// </summary>
        public Dictionary<string, Type> Providers
        {
            get
            {
                store.Scan(ProvidersDirectory);
                return store.Providers;
            }
        }

        /// <summary>
        /// Gets the state of the desktop wallpaper update task.
        /// </summary>
        public TaskState TaskState => task.State;

        //-----------------------------------------------------------------METHODS

        /// <summary>
        /// Constructs a <see cref="DailyDesktopCore"/> and calls
        /// <see cref="LoadSettings()"/>.
        /// </summary>
        /// <param name="providersDir">The directory to automatically scan <see cref="IProvider"/> DLL modules from.</param>
        /// <param name="serializeJsonDir">The directory to save JSON serializations in.</param>
        /// <param name="taskName">The name of the task registered in the Windows Task Scheduler.</param>
        /// <param name="autoCreateTask">Whether or not to automatically create the task when settings are changed or loaded.</param>
        public DailyDesktopCore(string providersDir, string serializeJsonDir, string taskName, bool autoCreateTask)
        {
            ProvidersDirectory = providersDir;
            SerializeJsonDirectory = serializeJsonDir;

            store = new ProviderStore();

            LoadSettings();
            AutoCreateTask = autoCreateTask;
            TaskName = taskName;
        }

        /// <summary>
        /// Creates a <see cref="Task"/> under the name <see cref="TaskName"/>. If
        /// one already exists, then it is updated instead.
        /// </summary>
        public void CreateTask()
        {
            string userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            TaskDefinition taskDefinition = TaskService.Instance.NewTask();
            taskDefinition.Settings.StartWhenAvailable = true;
#if (!DEBUG)
            taskDefinition.Settings.Hidden = true;
#endif
            if (settings.Enabled)
            {
                DailyTrigger dailyTrigger = new DailyTrigger
                {
                    DaysInterval = 1,
                    StartBoundary = settings.UpdateTime,
                };
                LogonTrigger logonTrigger = new LogonTrigger
                {
                    UserId = userId,
                };
                taskDefinition.Triggers.Add(dailyTrigger);
                taskDefinition.Triggers.Add(logonTrigger);
            }

            string args = $"\"{settings.DllPath}\" --json \"{WallpaperInfoJsonPath}\"";
            if (settings.DoResize)
                args += " --resize";
            if (settings.DoBlurredFit)
                args += $" --blur {settings.BlurStrength}";
            ExecAction execAction = new ExecAction
            {
                Path = getTaskExecutablePath(),
                Arguments = args,
            };
            taskDefinition.Actions.Add(execAction);

            task = TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
        }

        /// <summary>
        /// Deletes the wallpaper update <see cref="Task"/>. Doing so invalidates
        /// many member properties until <see cref="CreateTask"/> is called
        /// again.
        /// </summary>
        public void DeleteTask()
        {
            if (task != null)
                TaskService.Instance.RootFolder.DeleteTask(task.Name, false);
        }

        /// <summary>
        /// Manually triggers the wallpaper update <see cref="Task"/>, updating
        /// the desktop wallpaper using <see cref="CurrentProvider"/>.
        /// </summary>
        public void UpdateWallpaper()
        {
            task.Run();
        }

        /// <summary>
        /// Serializes settings to a JSON file at <see cref="SettingsJsonPath"/>.
        /// </summary>
        public void SaveSettings()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(SettingsJsonPath, jsonString);
        }

        /// <summary>
        /// Deserializes settings from a JSON file at
        /// <see cref="SettingsJsonPath"/> and loads them using
        /// <see cref="LoadSettings(DailyDesktopSettings)"/>.
        /// </summary>
        public void LoadSettings()
        {
            if (File.Exists(SettingsJsonPath))
            {
                string jsonString = File.ReadAllText(SettingsJsonPath);
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                };
                DailyDesktopSettings newSettings = JsonSerializer.Deserialize<DailyDesktopSettings>(jsonString, options);
                LoadSettings(newSettings);
            }
            else
            {
                LoadSettings(DailyDesktopSettings.Default);
            }
        }

        /// <summary>
        /// Loads settings from a <see cref="DailyDesktopSettings"/>.
        /// </summary>
        /// <param name="newSettings">The new settings to set.</param>
        public void LoadSettings(DailyDesktopSettings newSettings)
        {
            settings = newSettings;
            if (File.Exists(settings.DllPath))
            {
                Type providerType = store.Add(settings.DllPath);
                currentProvider = IProvider.Instantiate(providerType);
            }

            if (AutoCreateTask)
                CreateTask();
        }

        private string getTaskExecutablePath()
        {
            string baseDir = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).LocalPath;

            string[] paths = Directory.GetFiles(baseDir, TASK_EXECUTABLE_FILENAME, SearchOption.AllDirectories);
            if (paths.Length < 1)
                throw new IOException($"Did not find task executable {TASK_EXECUTABLE_FILENAME}.");

            return paths[0];
        }
    }
}
