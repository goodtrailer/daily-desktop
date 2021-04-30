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
    /// Scans, loads, and instantiates <see cref="IProvider"/> implementations
    /// from DLL modules placed in <see cref="ProvidersDirectory"/> and stores
    /// them in a <see cref="Dictionary{TKey, TValue}"/>. <br />
    /// <br />
    /// TKey is <see cref="string"/>.<br />
    /// TValue is <see cref="IProvider"/>.
    /// </summary>
    public class ProviderStore
    {
        private const string PROVIDERS_DIR = "providers";
        private const string PROVIDERS_SEARCH_PATTERN = "*.dll";

        /// <summary>
        /// Dictionary of <see cref="IProvider"/>s loaded from DLL modules found
        /// in <see cref="ProvidersDirectory"/>.
        /// </summary>
        public readonly Dictionary<string, IProvider> Providers;

        /// <summary>
        /// The providers directory where <see cref="IProvider"/> DLL modules
        /// are scanned from.
        /// </summary>
        public readonly string ProvidersDirectory;

        /// <summary>
        /// Constructs a <see cref="ProviderStore"/> and immediately calls
        /// <see cref="Scan"/>.
        /// </summary>
        public ProviderStore()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ProvidersDirectory = Path.Combine(appDataDir, "Daily Desktop", PROVIDERS_DIR);
            Providers = new Dictionary<string, IProvider>();

            Scan();
        }

        /// <summary>
        /// Scans <see cref="ProvidersDirectory"/> for any <see cref="IProvider"/>
        /// DLL modules and loads them into <see cref="Providers"/>. Any
        /// <see cref="IProvider"/>s previously contained in
        /// <see cref="Providers"/> will be cleared.
        /// </summary>
        public void Scan()
        {
            Providers.Clear();

            string[] paths;
            try
            {
                Directory.CreateDirectory(ProvidersDirectory);
                paths = Directory.GetFiles(ProvidersDirectory, PROVIDERS_SEARCH_PATTERN, SearchOption.AllDirectories);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
                paths = new string[0];
            }

            foreach (string path in paths)
            {
                try
                {
                    IProvider provider = instantiateProviderFromAssembly(path);
                    Providers.Add(provider.Key, provider);
                }
                catch (ProviderException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                catch (ReflectionTypeLoadException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private IProvider instantiateProviderFromAssembly(string fullPath)
        {
            var assembly = Assembly.LoadFile(fullPath);

            Type[] types = assembly.GetTypes();
            Type providerType = assembly.GetTypes().First(type =>
            {
                bool isPublic = type.IsPublic;
                bool isProvider = type.GetInterfaces().Contains(typeof(IProvider));
                return isPublic && isProvider;
            });

            IProvider provider = Activator.CreateInstance(providerType) as IProvider;

            if (provider == null)
                throw new ProviderException("Failed to instantiate an IProvider from the assembly.");
            if (provider.Key.Any(Char.IsWhiteSpace))
                throw new ProviderException("IProvider.Key contains whitespace.");

            return provider;
        }
    }
}
