using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Furion
{
    /// <summary>
    /// 插件化组件Context
    /// </summary>
    internal class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public PluginLoadContext(string pluginPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                using FileStream file = new FileStream(assemblyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                return LoadFromStream(file);
            }

            return null;
        }
    }
}
