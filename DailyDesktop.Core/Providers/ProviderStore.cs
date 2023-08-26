// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DailyDesktop.Core.Providers
{
    /// <summary>
    /// Scans and stores <see cref="IProvider"/> DLL modules and
    /// instantiates their <see cref="IProvider"/> implementation.
    /// </summary>
    public class ProviderStore
    {
        private const string PROVIDERS_SEARCH_PATTERN = "*.dll";

        /// <summary>
        /// Dictionary of <see cref="IProvider"/> <see cref="Type"/>s
        /// added through <see cref="ScanAsync"/> and <see cref="AddAsync"/>.
        /// Key is DLL module path.
        /// </summary>
        public IReadOnlyDictionary<string, Type> Providers => providers;
        private readonly Dictionary<string, Type> providers = new Dictionary<string, Type>();

        /// <summary>
        /// Clears <see cref="Providers"/>.
        /// </summary>
        public void Clear() => providers.Clear();

        /// <summary>
        /// Adds one DLL module to <see cref="Providers"/>. Will not
        /// do anything if the DLL module has already been added
        /// before.
        /// </summary>
        /// <param name="dllPath">The path of the <see cref="IProvider"/> DLL module to add</param>
        /// <returns>The <see cref="IProvider"/> implementation <see cref="Type"/>.</returns>
        public Type Add(string dllPath)
        {
            if (Providers.ContainsKey(dllPath))
                return Providers[dllPath];

            var assembly = Assembly.Load(File.ReadAllBytes(dllPath));
            return addImpl(dllPath, assembly);
        }

        /// <summary>
        /// Asynchronously adds one DLL module to <see cref="Providers"/>. Will not
        /// do anything if the DLL module has already been added
        /// before.
        /// </summary>
        /// <param name="dllPath">The path of the <see cref="IProvider"/> DLL module to add</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="IProvider"/> implementation <see cref="Type"/>.</returns>
        public async Task<Type> AddAsync(string dllPath, CancellationToken cancellationToken)
        {
            if (Providers.ContainsKey(dllPath))
                return Providers[dllPath];

            var assembly = Assembly.Load(await File.ReadAllBytesAsync(dllPath, cancellationToken));
            return addImpl(dllPath, assembly);
        }

        private Type addImpl(string dllPath, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                bool isPublic = type.IsPublic;
                bool isProvider = type.GetInterfaces().Contains(typeof(IProvider));
                if (isPublic && isProvider)
                {
                    providers.Add(dllPath, type);
                    return type;
                }
            }

            throw new TypeLoadException($"No {nameof(IProvider)} implementation found in \"{dllPath}\".");
        }

        /// <summary>
        /// Scans a directory for any <see cref="IProvider"/> DLL
        /// modules and adds their paths to <see cref="Providers"/>.
        /// </summary>
        /// <param name="directory">The directory to scan</param>
        public void Scan(string directory)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException($"{directory} does not exist");

            foreach (var path in Directory.GetFiles(directory, PROVIDERS_SEARCH_PATTERN, SearchOption.AllDirectories))
            {
                try
                {
                    Add(path);
                }
                catch (SystemException se)
                {
                    Console.WriteLine(se.StackTrace);
                }
            }
        }

        /// <summary>
        /// Asynchronously scans a directory for any <see cref="IProvider"/> DLL
        /// modules and adds their paths to <see cref="Providers"/>.
        /// </summary>
        /// <param name="directory">The directory to scan</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public async Task ScanAsync(string directory, CancellationToken cancellationToken)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException($"{directory} does not exist");

            foreach (var path in Directory.GetFiles(directory, PROVIDERS_SEARCH_PATTERN, SearchOption.AllDirectories))
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();

                try
                {
                    await AddAsync(path, cancellationToken);
                }
                catch (SystemException se)
                {
                    Console.WriteLine(se.StackTrace);
                }
            }
        }
    }
}
