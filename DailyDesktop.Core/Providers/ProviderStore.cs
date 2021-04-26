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

        public readonly Dictionary<string, IProvider> Providers = new Dictionary<string, IProvider>();

        public ProviderStore()
        {
            Scan();
        }

        public void Scan()
        {
            Providers.Clear();

            string baseDir = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string providersDir = Path.Combine(baseDir, PROVIDERS_DIR);

            string[] paths = Directory.GetFiles(providersDir, $"{PROVIDERS_ASSEMBLY_PREFIX}.*.dll", SearchOption.AllDirectories);
            foreach (string path in paths)
            {
                try
                {
                    IProvider provider = instantiateProviderFromAssembly(path);
                    Providers.Add(provider.Key, provider);
                }
                catch (Exception e)
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
                throw new Exception("Failed to instantiate an IProvider from the assembly.");

            return provider;
        }
    }
}
