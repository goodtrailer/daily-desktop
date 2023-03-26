// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyDesktop.Core.Configuration
{
    public abstract class AbstractConfiguration<T> : IReadOnlyConfiguration
        where T : IReadOnlyConfiguration
    {
        public AbstractConfiguration(string jsonPath)
        {
            JsonPath = jsonPath;
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public virtual string JsonPath { get; set; }
        
        /// <inheritdoc/>
        [JsonIgnore]
        public virtual bool IsAutoSerializing { get; set; }

        public event EventHandler? OnUpdate;

        public event EventHandler? OnSerialize;

        /// <inheritdoc/>
        public void Deserialize()
        {
            string jsonString = File.ReadAllText(JsonPath);
            var options = new JsonSerializerOptions();
            ConfigureDeserializer(options);

            var newConfig = JsonSerializer.Deserialize<T>(jsonString, options);

            Load(newConfig ?? throw new NullReferenceException("Deserialized config was null."));
        }
        
        public abstract void Load(T other);

        /// <inheritdoc/>
        public void Update()
        {
            OnUpdate?.Invoke(this, new EventArgs());

            if (IsAutoSerializing)
                Serialize();
        }

        /// <inheritdoc/>
        public void Serialize()
        {
            if (!(this is T @this))
                throw new InvalidOperationException($"this is not of type {nameof(T)}: {typeof(T).FullName}.");

            if (string.IsNullOrWhiteSpace(JsonPath))
                return;

            var options = new JsonSerializerOptions();
            ConfigureSerializer(options);

            string jsonString = JsonSerializer.Serialize(@this, options);
            File.WriteAllText(JsonPath, jsonString);

            OnSerialize?.Invoke(this, new EventArgs());
        }

        protected virtual void ConfigureSerializer(JsonSerializerOptions options)
        {
            options.WriteIndented = true;
        }

        protected virtual void ConfigureDeserializer(JsonSerializerOptions options)
        {
            options.AllowTrailingCommas = true;
        }
    }
}
