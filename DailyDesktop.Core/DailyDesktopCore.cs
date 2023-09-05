// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;

using Task = System.Threading.Tasks.Task;

namespace DailyDesktop.Core
{
    /// <summary>
    /// Core of Daily Desktop that handles the wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/>
    /// using Windows Task Scheduler. Also handles <see cref="IProvider"/> DLL
    /// module scanning, though <see cref="ProviderStore"/> is fully functional as
    /// a standalone class.
    /// </summary>
    public sealed class DailyDesktopCore : IDisposable
    {
        private Microsoft.Win32.TaskScheduler.Task? task;
        private string taskName;

        private readonly ProviderStore store = new();

        /// <summary>
        /// Read-only interface to the path configuration.
        /// </summary>
        public IReadOnlyPathConfiguration PathConfig => pathConfig;
        private readonly PathConfiguration pathConfig;

        /// <summary>
        /// Read-and-write interface to the task configuration.
        /// </summary>
        public IPublicTaskConfiguration TaskConfig => taskConfig;
        private readonly TaskConfiguration taskConfig;

        /// <summary>
        /// Whether or not to automatically create the task on when settings are changed or loaded.
        /// </summary>
        public bool IsAutoCreatingTask { get; set; }

        /// <summary>
        /// Name of the task registered in the Windows Task Scheduler.
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
        /// The currently selected provider corresponding to <see cref="TaskConfiguration.Dll"/>.
        /// </summary>
        public IProvider? CurrentProvider => Providers.ContainsKey(taskConfig.Dll) ? IProvider.Instantiate(Providers[taskConfig.Dll]) : null;

        /// <summary>
        /// The collection of currently loaded <see cref="IProvider"/>s.
        /// </summary>
        public IReadOnlyDictionary<string, Type> Providers => store.Providers;

        /// <summary>
        /// The state of the desktop wallpaper update task.
        /// </summary>
        public TaskState TaskState => task?.State ?? throw new InvalidOperationException("Task has not been created yet.");

        /// <summary>
        /// Constructs a <see cref="DailyDesktopCore"/> and loads <see cref="TaskConfig"/>.
        /// </summary>
        /// <param name="pathConfig">The path configuration.</param>
        /// <param name="taskName">The name of the task registered in the Windows Task Scheduler.</param>
        /// <param name="isAutoCreatingTask">Whether or not to automatically create the task when settings are changed or loaded.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public static async Task<DailyDesktopCore> CreateCoreAsync(PathConfiguration pathConfig, string taskName, bool isAutoCreatingTask, CancellationToken cancellationToken)
        {
            var core = new DailyDesktopCore(pathConfig, taskName)
            {
                IsAutoCreatingTask = isAutoCreatingTask
            };

            core.taskConfig.OnUpdateAsync += core.onTaskConfigUpdateAsync;

            if (!await core.taskConfig.TryDeserializeAsync(cancellationToken))
                await core.taskConfig.UpdateAsync(cancellationToken);

            return core;
        }

        /// <summary>
        /// Scans for <see cref="IProvider"/>s and populates <see cref="Providers"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public async Task ScanProvidersAsync(CancellationToken cancellationToken) => await store.ScanAsync(pathConfig.ProvidersDir, cancellationToken);

        /// <summary>
        /// Creates a <see cref="Microsoft.Win32.TaskScheduler.Task"/> under the name <see cref="TaskName"/>. If
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
        /// Deletes the wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/>. Doing so invalidates
        /// many member properties until <see cref="CreateTask"/> is called
        /// again.
        /// </summary>
        public void DeleteTask()
        {
            if (task != null)
                TaskService.Instance.RootFolder.DeleteTask(task.Name, false);
        }

        /// <summary>
        /// Manually triggers the wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/>, updating
        /// the desktop wallpaper using <see cref="CurrentProvider"/>.
        /// </summary>
        public void UpdateWallpaper()
        {
            if (task == null)
                throw new InvalidOperationException("Task has not been created yet.");

            task.Run();
        }

        private DailyDesktopCore(PathConfiguration pathConfig, string taskName)
        {
            this.pathConfig = pathConfig;
            this.taskName = taskName;

            taskConfig = new TaskConfiguration(pathConfig.TaskConfigJson)
            {
                IsAutoSerializing = true,
            };
        }

        private CancellationTokenSource? previousTokenSource;

        private async Task onTaskConfigUpdateAsync(object? sender, EventArgs? args, CancellationToken cancellationToken)
        {
            if (IsAutoCreatingTask)
            {
                try
                {
                    previousTokenSource?.Cancel();
                    previousTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    await Task.Run(CreateTask, previousTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Win32 Task update operation cancelled.");
                }
            }
        }

        /// <summary>
        /// Disposal method (e.g. event unsubscription).
        /// </summary>
        public void Dispose()
        {
            taskConfig.OnUpdateAsync -= onTaskConfigUpdateAsync;
        }
    }
}
