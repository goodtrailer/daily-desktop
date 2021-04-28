using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DailyDesktop.Core.Providers
{
    public class ProviderStore
    {
        private const string PROVIDERS_DIR = "providers";
        private const string PROVIDERS_ASSEMBLY_PREFIX = "DailyDesktop.Core.Providers";

        public readonly Dictionary<string, IProvider> Providers;
        public readonly string ProvidersDirectory;

        public ProviderStore()
        {
            string baseDir = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            ProvidersDirectory = Path.Combine(baseDir, PROVIDERS_DIR);
            Providers = new Dictionary<string, IProvider>();

            Scan();
        }

        public void Scan()
        {
            Providers.Clear();

            string[] paths;
            try
            {
                Directory.CreateDirectory(ProvidersDirectory);
                paths = Directory.GetFiles(ProvidersDirectory, $"{PROVIDERS_ASSEMBLY_PREFIX}.*.dll", SearchOption.AllDirectories);
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
