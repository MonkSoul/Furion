using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Furion
{
    /// <summary>
    /// 插件化组件 Context
    /// </summary>
    internal class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pluginPath"></param>
        public PluginLoadContext(string pluginPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                using FileStream file = new(assemblyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                return LoadFromStream(file);
            }

            return null;
        }
    }
}