using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;

namespace DailyDesktop.Core
{
    public class DailyDesktopCore
    {
        //------------------------------------------------------------VARIABLES

        private const string TASK_NAME = "Daily Desktop";
        private const string DEFAULT_UPDATE_TIME = "12:00 AM";

        private readonly ProviderStore store;
        private readonly Task task;

        private IProvider currentProvider;
        private DateTime updateTime;

        //-----------------------------------------------------------PROPERTIES

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

        //--------------------------------------------------------------METHODS

        public DailyDesktopCore()
        {
            store = new ProviderStore();
            task = TaskService.Instance.FindTask(TASK_NAME);

            if (task == null)
            {
                TaskDefinition taskDefinition = TaskService.Instance.NewTask();
                taskDefinition.RegistrationInfo.Description = currentProvider.Description;
                taskDefinition.Settings.StartWhenAvailable = true;

                DailyTrigger newDailyTrigger = new DailyTrigger
                {
                    DaysInterval = 1,
                    StartBoundary = DateTime.Parse(DEFAULT_UPDATE_TIME),
                };
                LogonTrigger newLogonTrigger = new LogonTrigger
                {
                    UserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                };
                taskDefinition.Triggers.Add(newDailyTrigger);
                taskDefinition.Triggers.Add(newLogonTrigger);

                task = TaskService.Instance.RootFolder.RegisterTaskDefinition(TASK_NAME, taskDefinition);
            }
            else
            {
                string key = task.Definition.Data;
                store.Providers.TryGetValue(key, out currentProvider);

                updateTime = dailyTrigger.StartBoundary;
            }
        }

        public void UpdateWallpaper()
        {
            task.Run();
        }

        private void updateTask()
        {
            task.Definition.Data = currentProvider.Key;
            dailyTrigger.StartBoundary = updateTime;
        }

        private void deleteTask()
        {
            TaskService.Instance.RootFolder.DeleteTask(task.Name, false);
        }
    }
}
