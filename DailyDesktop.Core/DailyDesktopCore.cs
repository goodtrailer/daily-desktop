using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DailyDesktop.Core
{
    public class DailyDesktopCore
    {
        //---------------------------------------------------------------VARIABLES

        private const string TASK_NAME_PREFIX = "Daily Desktop";
        private const string TASK_EXECUTABLE = "DailyDesktop.Task.exe";
        private const string DEFAULT_UPDATE_TIME = "12:00 AM";

        private readonly ProviderStore store;
        private readonly Task task;

        private IProvider currentProvider;
        private DateTime updateTime;

        //--------------------------------------------------------------PROPERTIES

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

                task = TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
                currentProvider = null;
            }
            else
            {
                string key = execAction.Arguments;

                if (key != null && store.Providers.ContainsKey(key))
                    store.Providers.TryGetValue(key, out currentProvider);
                else
                    currentProvider = null;

                execAction.Path = taskExecutablePath;
                task.RegisterChanges();
            }

            updateTime = dailyTrigger.StartBoundary;
        }

        public void UpdateWallpaper()
        {
            task.Run();
        }

        private void updateTask()
        {
            dailyTrigger.StartBoundary = updateTime;
            execAction.Arguments = currentProvider?.Key ?? string.Empty;
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
