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

        /// <summary>
        /// Deserialize a JSON file (located at <see cref="JsonPath"/>) to a configuration.
        /// </summary>
        public void Deserialize()
        {
            if (string.IsNullOrWhiteSpace(JsonPath))
                throw new InvalidOperationException($"{JsonPath} is null or whitespace.");

            string jsonString = File.ReadAllText(JsonPath);
            var options = new JsonSerializerOptions();
            ConfigureDeserializer(options);

            var newConfig = JsonSerializer.Deserialize<T>(jsonString, options) ?? throw new NullReferenceException("Deserialized config was null.");

            bool temp = IsAutoSerializing;
            IsAutoSerializing = false;

            Load(newConfig);

            IsAutoSerializing = temp;
            Update();
        }

        /// <summary>
        /// Try to deserialize a JSON file (located at <see cref="JsonPath"/>) to a configuration.
        /// </summary>
        /// <returns>
        /// Whether or not the deserialiazation was successful.
        /// </returns>
        public bool TryDeserialize()
        {
            try
            {
                Deserialize();
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException || ex is InvalidOperationException || ex is NullReferenceException)
            {
                return false;
            }
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
                throw new InvalidCastException($"this is not of type {nameof(T)}: {typeof(T).FullName}.");

            if (string.IsNullOrWhiteSpace(JsonPath))
                throw new InvalidOperationException($"{JsonPath} is null or whitespace.");

            var options = new JsonSerializerOptions();
            ConfigureSerializer(options);

            string jsonString = JsonSerializer.Serialize(@this, options);
            File.WriteAllText(JsonPath, jsonString);

            OnSerialize?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        public bool TrySerialize()
        {
            try
            {
                Serialize();
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException || ex is InvalidOperationException)
            {
                return false;
            }
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
