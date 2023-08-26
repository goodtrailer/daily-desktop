// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Util;

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
        /// Whether or not serialization should occur automatically (e.g. on update). Note that automatic serialization is synchronous.
        /// </summary>
        bool IsAutoSerializing { get; }

        /// <summary>
        /// Asynchronous event published on calls to <see cref="UpdateAsync"/>.
        /// </summary>
        event AsyncEventHandler OnUpdateAsync;

        /// <summary>
        /// Asynchronous event published on successful calls to <see cref="SerializeAsync"/>.
        /// </summary>
        event AsyncEventHandler OnSerializeAsync;

        /// <summary>
        /// Automatically called upon setting properties. Can be called manually to asynchronously
        /// publish to <see cref="OnUpdateAsync"/> and serialize (in case <see cref="IsAutoSerializing"/> is true).
        /// </summary>
        Task UpdateAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize configuration to a JSON file (located at <see cref="JsonPath"/>).
        /// </summary>
        Task SerializeAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Try to asynchronously serialize configuration to a JSON file (located at <see cref="JsonPath"/>).
        /// </summary>
        /// <returns>
        /// Whether or not the serialiazation was successful.
        /// </returns>
        Task<bool> TrySerializeAsync(CancellationToken cancellationToken);
    }
}
