// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyDesktop.Core
{
    public struct DailyDesktopSettings
    {
        public string DllPath { get; set; }
        public bool Enabled { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool DoBlurredFit { get; set; }
        public int BlurStrength { get; set; }

        public static DailyDesktopSettings Default = new DailyDesktopSettings
        {
            DllPath = string.Empty,
            Enabled = true,
            UpdateTime = DateTime.Parse("12:00 AM"),
            DoBlurredFit = true,
            BlurStrength = 40,
        };
    }
}
