using Autofac;
using Fur.AppCore;
using Fur.AppCore.Attributes;
using System.Linq;

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 依赖注入初始化类
    /// </summary>
    [NonInflated]
    public sealed class Injection
    {
        /// <summary>
        /// 初始化程序集模块注册
        /// </summary>
        /// <param name="builder">容器构建器</param>
        public static void Initialize(ContainerBuilder builder)
            => builder.RegisterAssemblyModules(App.Inflations.Assemblies.Select(a => a.ThisAssembly).ToArray());
    }
}