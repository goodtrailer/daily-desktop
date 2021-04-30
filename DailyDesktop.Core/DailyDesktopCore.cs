// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private const string TASK_NAME_PREFIX = "Daily Desktop";
        private const string TASK_EXECUTABLE = "DailyDesktop.Task.exe";
        private const string DEFAULT_UPDATE_TIME = "12:00 AM";
        private const int DEFAULT_BLUR_STRENGTH = 40;

        private readonly ProviderStore store;
        private readonly string userId;
        private readonly string userName;
        private readonly string taskName;
        private Task task;

        private bool enabled;
        private bool doBlurredFit;
        private int blurStrength;
        private IProvider currentProvider;
        private DateTime updateTime;

        //--------------------------------------------------------------PROPERTIES

        /// <summary>
        /// Gets the providers directory where <see cref="IProvider"/> DLL modules
        /// are scanned from.
        /// </summary>
        public string ProvidersDirectory => store.ProvidersDirectory;

        /// <summary>
        /// Gets or sets whether wallpaper update <see cref="Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                updateTask();
            }
        }

        /// <summary>
        /// Gets or sets whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        public bool DoBlurredFit
        {
            get => doBlurredFit;
            set
            {
                doBlurredFit = value;
                updateTask();
            }
        }

        /// <summary>
        /// Gets or sets the blur strength for wallpaper images. Only applies if
        /// <see cref="DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        public int BlurStrength
        {
            get => blurStrength;
            set
            {
                blurStrength = value;
                updateTask();
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="IProvider"/> to fetch wallpaper
        /// image URIs from.
        /// </summary>
        public IProvider CurrentProvider
        {
            get => currentProvider;
            set
            {
                currentProvider = value;
                updateTask();
            }
        }

        /// <summary>
        /// Gets or sets the time at which the daily wallpaper update trigger
        /// executes. Only applies if <see cref="Enabled"/> is set to <c>true</c>.
        /// </summary>
        public DateTime UpdateTime
        {
            get => updateTime;
            set
            {
                updateTime = value;
                updateTask();
            }
        }

        /// <summary>
        /// Gets a freshly scanned <see cref="IProvider"/> collection loaded from
        /// DLL modules placed in <see cref="ProvidersDirectory"/>.
        /// </summary>
        public ICollection<IProvider> Providers
        {
            get
            {
                store.Scan();
                return store.Providers.Values;
            }
        }

        private DailyTrigger dailyTrigger
        {
            get
            {
                return task.Definition.Triggers.Find(trigger =>
                {
                    bool isDaily = trigger.TriggerType == TaskTriggerType.Daily;
                    return isDaily;
                }) as DailyTrigger;
            }
        }

        private LogonTrigger logonTrigger
        {
            get
            {
                return task.Definition.Triggers.Find(trigger =>
                {
                    bool isLogon = trigger.TriggerType == TaskTriggerType.Logon;
                    return isLogon;
                }) as LogonTrigger;
            }
        }

        private ExecAction execAction
        {
            get
            {
                return task.Definition.Actions.Find(action =>
                {
                    bool isExec = action.ActionType == TaskActionType.Execute;
                    return isExec;
                }) as ExecAction;
            }
        }

        private string[] execArguments
        {
            get => execAction.Arguments?.Split(" ");
            set
            {
                execAction.Arguments = string.Join(" ", value);
            }
        }

        private int execArgumentsCount => execArguments?.Length ?? 0;

        //-----------------------------------------------------------------METHODS

        /// <summary>
        /// Constructs a <see cref="DailyDesktopCore"/> and attempts to find the
        /// wallpaper update <see cref="Task"/>. If no registered
        /// <see cref="Task"/> is found, then it calls
        /// <see cref="CreateDefaultTask"/>. Data is loaded from the task into
        /// corresponding members, and the task executable path is updated to the
        /// current directory.
        /// </summary>
        public DailyDesktopCore()
        {
            store = new ProviderStore();

            userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userId.Split('\\').Last();
            taskName = $"{TASK_NAME_PREFIX}_{userName}";

            task = TaskService.Instance.FindTask(taskName);
            if (task == null)
                CreateDefaultTask();

            loadValuesFromTask();
            execAction.Path = getTaskExecutablePath();
            task.RegisterChanges();
        }

        /// <summary>
        /// Creates a <see cref="Task"/> using default values. If one has already
        /// been created, the new <see cref="Task"/> overrides the existing one.
        /// </summary>
        public void CreateDefaultTask()
        {
            TaskDefinition taskDefinition = TaskService.Instance.NewTask();
            taskDefinition.RegistrationInfo.Description = string.Empty;
            taskDefinition.Settings.StartWhenAvailable = true;
#if (!DEBUG)
            taskDefinition.Settings.Hidden = true;
#endif

            DailyTrigger newDailyTrigger = new DailyTrigger
            {
                DaysInterval = 1,
                StartBoundary = DateTime.Parse(DEFAULT_UPDATE_TIME),
            };
            LogonTrigger newLogonTrigger = new LogonTrigger
            {
                UserId = userId,
            };
            taskDefinition.Triggers.Add(newDailyTrigger);
            taskDefinition.Triggers.Add(newLogonTrigger);

            ExecAction newExecAction = new ExecAction
            {
                Path = getTaskExecutablePath(),
                Arguments = string.Empty,
            };
            taskDefinition.Actions.Add(newExecAction);

            task = TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
        }

        /// <summary>
        /// Deletes the wallpaper update <see cref="Task"/>. Doing so invalidates
        /// many member properties until <see cref="CreateDefaultTask"/> is called
        /// again.
        /// </summary>
        public void DeleteTask()
        {
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

        private void loadValuesFromTask()
        {
            if (execArgumentsCount >= 1 && store.Providers.ContainsKey(execArguments[0]))
                store.Providers.TryGetValue(execArguments[0], out currentProvider);

            enabled = dailyTrigger.Enabled;
            blurStrength = DEFAULT_BLUR_STRENGTH;
            doBlurredFit = execArgumentsCount >= 2 && int.TryParse(execArguments[1], out blurStrength);
            updateTime = dailyTrigger.StartBoundary;
        }

        private void updateTask()
        {
            dailyTrigger.StartBoundary = updateTime;
            string key = currentProvider?.Key ?? string.Empty;
            string blur = doBlurredFit ? blurStrength.ToString() : string.Empty;
            execArguments = new string[] { key, blur };
            dailyTrigger.Enabled = enabled;
            logonTrigger.Enabled = enabled;
            task.RegisterChanges();
        }

        private string getTaskExecutablePath()
        {
            string baseDir = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;

            string[] paths = Directory.GetFiles(baseDir, TASK_EXECUTABLE, SearchOption.AllDirectories);
            if (paths.Length < 1)
                throw new IOException($"Did not find task executable {TASK_EXECUTABLE}.");

            return paths[0];
        }
    }
}
