// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Text.Json.Serialization;

namespace DailyDesktop.Core.Configuration
{
    public interface IReadOnlyConfiguration
    {
        [JsonIgnore]
        string JsonPath { get; }

        [JsonIgnore]
        bool IsAutoSerializing { get; }

        event EventHandler OnUpdate;

        event EventHandler OnSerialize;

        void Update();

        void Serialize();
    }
}
