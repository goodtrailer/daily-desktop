// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to path settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public interface IPublicPathConfiguration : IReadOnlyPathConfiguration
    {
        /// <summary>
        /// The assembly directory (e.g. for the task executable).
        /// </summary>
        new string AssemblyDir { get; set; }

        /// <summary>
        /// The providers directory (e.g. for <see cref="IProvider"/> DLL modules).
        /// </summary>
        new string ProvidersDir { get; set; }

        /// <summary>
        /// The serialization directory (e.g. for the <see cref="TaskConfiguration"/> JSON).
        /// </summary>
        new string SerializationDir { get; set; }
        
        /// <summary>
        /// Asynchronously sets the assembly directory (e.g. for the task executable).
        /// </summary>
        /// <param name="assemblyDir">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetAssemblyDirAsync(string assemblyDir, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the providers directory (e.g. for <see cref="IProvider"/> DLL modules).
        /// </summary>
        /// <param name="providersDir">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetProvidersDirAsync(string providersDir, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the serialization directory (e.g. for the <see cref="TaskConfiguration"/> JSON).
        /// </summary>
        /// <param name="serializationDir">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetSerializationDirAsync(string serializationDir, CancellationToken cancellationToken);
    }
}
