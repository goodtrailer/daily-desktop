// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Interface to configuration update and serialization.
    /// </summary>
    public interface IReadOnlyConfiguration
    {
        /// <summary>
        /// The path to the JSON file (serialization output).
        /// </summary>
        string JsonPath { get; }

        /// <summary>
        /// Whether or not serialization should occur automatically (e.g. on update).
        /// </summary>
        bool IsAutoSerializing { get; }

        /// <summary>
        /// Event published on calls to <see cref="Update"/>.
        /// </summary>
        event EventHandler OnUpdate;

        /// <summary>
        /// Event published on successful calls to <see cref="Serialize"/>.
        /// </summary>
        event EventHandler OnSerialize;

        /// <summary>
        /// Automatically called upon setting properties (can be called manually to serialize in case <see cref="IsAutoSerializing"/> is true).
        /// </summary>
        void Update();

        /// <summary>
        /// Serialize configuration to a JSON file (located at <see cref="JsonPath"/>).
        /// </summary>
        void Serialize();

        /// <summary>
        /// Try to serialize configuration to a JSON file (located at <see cref="JsonPath"/>).
        /// </summary>
        /// <returns>
        /// Whether or not the serialiazation was successful.
        /// </returns>
        bool TrySerialize();
    }
}
