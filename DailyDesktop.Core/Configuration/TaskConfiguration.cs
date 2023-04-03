// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains task settings for <see cref="DailyDesktopCore"/> that is meant to be serialized.
    /// </summary>
    public class TaskConfiguration : AbstractConfiguration<TaskConfiguration>, IPublicTaskConfiguration
    {
        /// <inheritdoc/>
        public TaskConfiguration(string jsonPath = "")
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
        public async Task SetDllAsync(string dll, CancellationToken cancellationToken)
        {
            if (this.dll == dll)
                return;

            this.dll = dll;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetIsEnabledAsync(bool isEnabled, CancellationToken cancellationToken)
        {
            if (this.isEnabled == isEnabled)
                return;

            this.isEnabled = isEnabled;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetUpdateTimeAsync(DateTime updateTime, CancellationToken cancellationToken)
        {
            if (this.updateTime == updateTime)
                return;

            this.updateTime = updateTime;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetDoResizeAsync(bool doResize, CancellationToken cancellationToken)
        {
            if (this.doResize == doResize)
                return;

            this.doResize = doResize;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetDoBlurredFitAsync(bool doBlurredFit, CancellationToken cancellationToken)
        {
            if (this.doBlurredFit == doBlurredFit)
                return;

            this.doBlurredFit = doBlurredFit;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetBlurStrengthAsync(int blurStrength, CancellationToken cancellationToken)
        {
            if (this.blurStrength == blurStrength)
                return;

            this.blurStrength = blurStrength;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override void LoadImpl(TaskConfiguration other)
        {
            dll = other.dll;
            isEnabled = other.isEnabled;
            updateTime = other.updateTime;
            doResize = other.doResize;
            doBlurredFit = other.doBlurredFit;
            blurStrength = other.blurStrength;
        }

        private string dll = "";
        private bool isEnabled = true;
        private DateTime updateTime = DateTime.Parse("12:00 AM");
        private bool doResize = true;
        private bool doBlurredFit = true;
        private int blurStrength = 40;
    }
}
