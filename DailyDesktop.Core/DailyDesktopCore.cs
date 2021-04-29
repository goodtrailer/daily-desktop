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
    public class DailyDesktopCore
    {
        //---------------------------------------------------------------VARIABLES

        private const string TASK_NAME_PREFIX = "Daily Desktop";
        private const string TASK_EXECUTABLE = "DailyDesktop.Task.exe";
        private const string DEFAULT_UPDATE_TIME = "12:00 AM";
        private const int DEFAULT_BLUR_STRENGTH = 40;

        private readonly ProviderStore store;
        private readonly Task task;

        private bool enabled;
        private bool doBlurredFit;
        private int blurStrength;
        private IProvider currentProvider;
        private DateTime updateTime;

        //--------------------------------------------------------------PROPERTIES

        public string ProvidersDirectory => store.ProvidersDirectory;

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                updateTask();
            }
        }

        public bool DoBlurredFit
        {
            get => doBlurredFit;
            set
            {
                doBlurredFit = value;
                updateTask();
            }
        }

        public int BlurStrength
        {
            get => blurStrength;
            set
            {
                blurStrength = value;
                updateTask();
            }
        }

        public IProvider CurrentProvider
        {
            get => currentProvider;
            set
            {
                currentProvider = value;
                updateTask();
            }
        }

        public DateTime UpdateTime
        {
            get => updateTime;
            set
            {
                updateTime = value;
                updateTask();
            }
        }

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

        public DailyDesktopCore()
        {
            store = new ProviderStore();

            string userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName = userId.Split('\\').Last();
            string taskName = $"{TASK_NAME_PREFIX}_{userName}";

            task = TaskService.Instance.FindTask(taskName);

            string taskExecutablePath = getTaskExecutablePath();

            if (task == null)
            {
                TaskDefinition taskDefinition = TaskService.Instance.NewTask();
                taskDefinition.RegistrationInfo.Description = string.Empty;
                taskDefinition.Settings.StartWhenAvailable = true;

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
                    Path = taskExecutablePath,
                    Arguments = string.Empty,
                };
                taskDefinition.Actions.Add(newExecAction);

#if (!DEBUG)
                taskDefinition.Settings.Hidden = true;
#endif

                task = TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
            }
            else
            {
                string key = (execArgumentsCount < 1) ? null : execArguments[0];

                if (key != null && store.Providers.ContainsKey(key))
                    store.Providers.TryGetValue(key, out currentProvider);

                execAction.Path = taskExecutablePath;
                task.RegisterChanges();
            }

            enabled = dailyTrigger.Enabled;
            updateTime = dailyTrigger.StartBoundary;

            if (execArgumentsCount >= 2 && int.TryParse(execArguments[1], out blurStrength))
            {
                doBlurredFit = true;
            } else
            {
                doBlurredFit = false;
                blurStrength = DEFAULT_BLUR_STRENGTH;
            }
        }

        public void UpdateWallpaper()
        {
            task.Run();
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

        private void deleteTask()
        {
            TaskService.Instance.RootFolder.DeleteTask(task.Name, false);
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
