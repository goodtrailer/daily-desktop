// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
        /// added through <see cref="Scan"/> and <see cref="Add"/>.
        /// Key is DLL module path.
        /// </summary>
        public readonly Dictionary<string, Type> Providers = new Dictionary<string, Type>();

        /// <summary>
        /// Clears <see cref="Providers"/>. Identical to directly
        /// calling <see cref="List{T}.Clear"/> on
        /// <see cref="Providers"/>.
        /// </summary>
        public void Clear() => Providers.Clear();

        /// <summary>
        /// Adds one DLL module to <see cref="Providers"/>. Will not
        /// do anything if the DLL module has already been added
        /// before.
        /// </summary>
        /// <param name="dllPath">The path of the <see cref="IProvider"/> DLL module to add</param>
        /// <returns>The <see cref="IProvider"/> implementation <see cref="Type"/>.</returns>
        public Type? Add(string dllPath)
        {
            if (Providers.ContainsKey(dllPath))
                return Providers[dllPath];

            var assembly = Assembly.LoadFile(dllPath);

            foreach (Type type in assembly.GetTypes())
            {
                bool isPublic = type.IsPublic;
                bool isProvider = type.GetInterfaces().Contains(typeof(IProvider));
                if (isPublic && isProvider)
                {
                    Providers.Add(dllPath, type);
                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// Scans a directory for any <see cref="IProvider"/> DLL
        /// modules and adds their paths to <see cref="Providers"/>.
        /// </summary>
        /// <param name="directory">The directory to scan</param>
        public void Scan(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] paths = Directory.GetFiles(directory, PROVIDERS_SEARCH_PATTERN, SearchOption.AllDirectories);
                foreach (var path in paths)
                {
                    try
                    {
                        Add(path);
                    }
                    catch (ReflectionTypeLoadException) { }
                }
            }
        }
    }
}
