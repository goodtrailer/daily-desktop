// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Util;

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
        public AbstractConfiguration(string jsonPath = "")
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
        public event AsyncEventHandler? OnUpdateAsync;

        /// <inheritdoc/>
        public event AsyncEventHandler? OnSerializeAsync;

        /// <summary>
        /// Deserialize a JSON file (located at <see cref="JsonPath"/>) to a configuration.
        /// </summary>
        public void Deserialize()
        {
            if (string.IsNullOrWhiteSpace(JsonPath))
                throw new InvalidOperationException($"{JsonPath} is null or whitespace.");

            var options = new JsonSerializerOptions();
            ConfigureDeserializer(options);

            T newConfig;
            using (var jsonStream = File.OpenRead(JsonPath))
                newConfig = JsonSerializer.Deserialize<T>(jsonStream, options) ?? throw new NullReferenceException("Deserialized config was null.");

            bool temp = IsAutoSerializing;
            IsAutoSerializing = false;

            Load(newConfig);

            IsAutoSerializing = temp;
        }

        /// <summary>
        /// Asynchronously deserialize a JSON file (located at <see cref="JsonPath"/>) to a configuration.
        /// </summary>
        public async Task DeserializeAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(JsonPath))
                throw new InvalidOperationException($"{JsonPath} is null or whitespace.");

            var options = new JsonSerializerOptions();
            ConfigureDeserializer(options);

            T newConfig;
            using (var jsonStream = File.OpenRead(JsonPath))
                newConfig = await JsonSerializer.DeserializeAsync<T>(jsonStream, options) ?? throw new NullReferenceException("Deserialized config was null.");

            bool temp = IsAutoSerializing;
            IsAutoSerializing = false;

            await LoadAsync(newConfig, cancellationToken);

            IsAutoSerializing = temp;
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
            catch (Exception ex) when (ex is JsonException
                || ex is IOException
                || ex is SystemException
                || ex is InvalidOperationException
                || ex is NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Try to asynchronously deserialize a JSON file (located at <see cref="JsonPath"/>) to a configuration.
        /// </summary>
        /// <returns>
        /// Whether or not the deserialiazation was successful.
        /// </returns>
        public async Task<bool> TryDeserializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await DeserializeAsync(cancellationToken);
                return true;
            }
            catch (Exception ex) when (ex is JsonException
                || ex is IOException
                || ex is SystemException
                || ex is InvalidOperationException
                || ex is NullReferenceException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public void Update()
        {
            OnUpdate?.Invoke(this, EventArgs.Empty);

            if (IsAutoSerializing)
                Serialize();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(CancellationToken cancellationToken)
        {
            await OnUpdateAsync.InvokeAsync(this, EventArgs.Empty, cancellationToken);

            if (IsAutoSerializing)
                await SerializeAsync(cancellationToken);
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

            using (var jsonStream = File.Create(JsonPath))
                JsonSerializer.Serialize(jsonStream, @this, options);

            OnSerialize?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        public async Task SerializeAsync(CancellationToken cancellationToken)
        {
            if (!(this is T @this))
                throw new InvalidCastException($"this is not of type {nameof(T)}: {typeof(T).FullName}.");

            if (string.IsNullOrWhiteSpace(JsonPath))
                throw new InvalidOperationException($"{JsonPath} is null or whitespace.");

            var options = new JsonSerializerOptions();
            ConfigureSerializer(options);

            using (var jsonStream = File.Create(JsonPath))
                await JsonSerializer.SerializeAsync(jsonStream, @this, options);

            await OnSerializeAsync.InvokeAsync(this, EventArgs.Empty, cancellationToken);
        }

        /// <inheritdoc/>
        public bool TrySerialize()
        {
            try
            {
                Serialize();
                return true;
            }
            catch (Exception ex) when (ex is JsonException
                || ex is IOException
                || ex is SystemException
                || ex is InvalidOperationException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> TrySerializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SerializeAsync(cancellationToken);
                return true;
            }
            catch (Exception ex) when (ex is JsonException
                || ex is IOException
                || ex is SystemException
                || ex is InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Loads options from another configuration instance. Basically like
        /// a copy constructor/method.
        /// </summary>
        /// <param name="other">The other configuration instance to copy options from.</param>
        public void Load(T other)
        {
            LoadImpl(other);
            Update();
        }

        /// <summary>
        /// Asynchronously loads options from another configuration instance. Basically like
        /// a copy constructor/method.
        /// </summary>
        /// <param name="other">The other configuration instance to copy options from.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public async Task LoadAsync(T other, CancellationToken cancellationToken)
        {
            LoadImpl(other);
            await UpdateAsync(cancellationToken);
        }

        /// <summary>
        /// Implementation of <see cref="Load(T)"/>/<see cref="LoadAsync(T, CancellationToken)"/>.
        /// Should do the actual value copying.
        /// </summary>
        /// <param name="other">The other configuration instance to copy options from.</param>
        protected abstract void LoadImpl(T other);

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
