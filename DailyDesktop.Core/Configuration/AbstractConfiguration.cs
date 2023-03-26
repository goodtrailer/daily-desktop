// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Abstract base class to ease implementation of <see cref="IReadOnlyConfiguration"/>,
    /// mainly with respect to serialization/deserialization.
    /// </summary>
    /// <typeparam name="T">The type of the implementor (somewhat like CRTP).</typeparam>
    public abstract class AbstractConfiguration<T> : IReadOnlyConfiguration
        where T : IReadOnlyConfiguration
    {
        /// <summary>
        /// Sets the JSON path.
        /// </summary>
        /// <param name="jsonPath">The path of the JSON file to serialize to.</param>
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

        /// <inheritdoc/>
        public event EventHandler? OnUpdate;

        /// <inheritdoc/>
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
        
        /// <summary>
        /// Loads options from another configuration instance. Basically like
        /// a copy constructor/method.
        /// </summary>
        /// <param name="other">The other configuration instance to copy options from.</param>
        public abstract void Load(T other);

        /// <summary>
        /// Configures the serializer options.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        protected virtual void ConfigureSerializer(JsonSerializerOptions options)
        {
            options.WriteIndented = true;
        }

        /// <summary>
        /// Configures the deserializer options.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        protected virtual void ConfigureDeserializer(JsonSerializerOptions options)
        {
            options.AllowTrailingCommas = true;
        }
    }
}
