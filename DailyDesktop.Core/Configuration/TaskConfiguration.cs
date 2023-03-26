// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Interface to task settings for <see cref="DailyDesktopCore"/> that is meant to be serialized.
    /// </summary>
    public interface ITaskConfiguration : IReadOnlyTaskConfiguration
    {
        /// <inheritdoc/>
        new string Dll { get; set; }

        /// <inheritdoc/>
        new bool IsEnabled { get; set; }
        
        /// <inheritdoc/>
        new DateTime UpdateTime { get; set; }
        
        /// <inheritdoc/>
        new bool DoResize { get; set; }
        
        /// <inheritdoc/>
        new bool DoBlurredFit { get; set; }
        
        /// <inheritdoc/>
        new int BlurStrength { get; set; }

    }

    /// <summary>
    /// Contains task settings for <see cref="DailyDesktopCore"/> that is meant to be serialized.
    /// </summary>
    public class TaskConfiguration : AbstractConfiguration<TaskConfiguration>, ITaskConfiguration
    {
        /// <inheritdoc/>
        public TaskConfiguration(string jsonPath)
            : base(jsonPath)
        {
        }

        /// <inheritdoc/>
        public string Dll
        {
            get => dll;
            set
            {
                if (dll == value)
                    return;

                dll = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value)
                    return;

                isEnabled = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public DateTime UpdateTime
        {
            get => updateTime;
            set
            {
                if (updateTime == value)
                    return;

                updateTime = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public bool DoResize
        {
            get => doResize;
            set
            {
                if (doResize == value)
                    return;

                doResize = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public bool DoBlurredFit
        {
            get => doBlurredFit;
            set
            {
                if (doBlurredFit == value)
                    return;

                doBlurredFit = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public int BlurStrength
        {
            get => blurStrength;
            set
            {
                if (blurStrength == value)
                    return;

                blurStrength = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public override void Load(TaskConfiguration other)
        {
            Dll = other.Dll;
            IsEnabled = other.IsEnabled;
            UpdateTime = other.UpdateTime;
            DoResize = other.DoResize;
            DoBlurredFit = other.DoBlurredFit;
            BlurStrength = other.BlurStrength;
        }

        private string dll = "";
        private bool isEnabled = true;
        private DateTime updateTime = DateTime.Parse("12:00 AM");
        private bool doResize = true;
        private bool doBlurredFit = true;
        private int blurStrength = 40;
    }
}
