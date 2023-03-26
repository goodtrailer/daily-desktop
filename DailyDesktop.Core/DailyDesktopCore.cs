// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using DailyDesktop.Core.Configuration;
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
    public class DailyDesktopCore : IDisposable
    {
        private readonly ProviderStore store = new ProviderStore();

        private Task? task;
        private string taskName;

        public IReadOnlyPathConfiguration PathConfig => pathConfig;
        private readonly PathConfiguration pathConfig;

        public ITaskConfiguration TaskConfig => taskConfig;
        private readonly TaskConfiguration taskConfig;
        
        /// <summary>
        /// Gets or sets whether or not to automatically create the task on
        /// when settings are changed or loaded.
        /// </summary>
        public bool IsAutoCreatingTask { get; set; }

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
                
                if (IsAutoCreatingTask)
                    CreateTask();
            }
        }

        /// <summary>
        /// Gets or sets the current provider to fetch wallpaper image URIs from
        /// in a <see cref="ProviderWrapper"/>.
        /// </summary>
        public IProvider? CurrentProvider => currentProvider;
        private IProvider? currentProvider;

        /// <summary>
        /// Gets a freshly scanned dictionary of <see cref="IProvider"/>
        /// <see cref="Type"/> values and DLL module path keys.
        /// </summary>
        public Dictionary<string, Type> Providers
        {
            get
            {
                store.Scan(pathConfig.ProvidersDir);
                return store.Providers;
            }
        }

        /// <summary>
        /// Gets the state of the desktop wallpaper update task.
        /// </summary>
        public TaskState TaskState => task?.State ?? throw new InvalidOperationException("Task has not been created yet.");

        /// <summary>
        /// Constructs a <see cref="DailyDesktopCore"/> and loads <see cref="TaskConfig"/>.
        /// </summary>
        /// <param name="pathConfig">The path configuration.</param>
        /// <param name="taskName">The name of the task registered in the Windows Task Scheduler.</param>
        /// <param name="isAutoCreatingTask">Whether or not to automatically create the task when settings are changed or loaded.</param>
        public DailyDesktopCore(PathConfiguration pathConfig, string taskName, bool isAutoCreatingTask)
        {
            this.pathConfig = pathConfig;
            this.taskName = taskName;
            IsAutoCreatingTask = isAutoCreatingTask;

            taskConfig = new TaskConfiguration(pathConfig.TaskConfigJson)
            {
                IsAutoSerializing = true,
            };
            taskConfig.OnUpdate += onTaskConfigUpdate;

            if (File.Exists(taskConfig.JsonPath))
                taskConfig.Deserialize();
            else
                taskConfig.Update();

            if (File.Exists(taskConfig.Dll))
            {
                var providerType = store.Add(taskConfig.Dll);

                if (providerType != null)
                    currentProvider = IProvider.Instantiate(providerType);
            }

            if (IsAutoCreatingTask)
                CreateTask();
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
            if (taskConfig.IsEnabled)
            {
                DailyTrigger dailyTrigger = new DailyTrigger
                {
                    DaysInterval = 1,
                    StartBoundary = taskConfig.UpdateTime,
                };
                LogonTrigger logonTrigger = new LogonTrigger
                {
                    UserId = userId,
                };
                taskDefinition.Triggers.Add(dailyTrigger);
                taskDefinition.Triggers.Add(logonTrigger);
            }

            string args = $"\"{taskConfig.Dll}\" --json \"{PathConfig.WallpaperJson}\"";

            if (taskConfig.DoResize)
                args += " --resize";

            if (taskConfig.DoBlurredFit)
                args += $" --blur {taskConfig.BlurStrength}";

            ExecAction execAction = new ExecAction
            {
                Path = PathConfig.TaskExecutable,
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
            if (task == null)
                throw new InvalidOperationException("Task has not been created yet.");

            task.Run();
        }

        private void onTaskConfigUpdate(object? _ = null, EventArgs? __ = null)
        {
            currentProvider = string.IsNullOrWhiteSpace(taskConfig.Dll) ? null : IProvider.Instantiate(Providers[taskConfig.Dll]);

            if (IsAutoCreatingTask)
                CreateTask();
        }

        public void Dispose()
        {
            taskConfig.OnUpdate -= onTaskConfigUpdate;
        }
    }
}
