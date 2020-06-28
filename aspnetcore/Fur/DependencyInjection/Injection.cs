using Autofac;
using Fur.ApplicationSystem;
using System.Linq;

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 依赖注入初始化类
    /// </summary>
    public sealed class Injection
    {
        #region 初始化程序集模块注册 +/* public static void Initialize(ContainerBuilder builder)
        /// <summary>
        /// 初始化程序集模块注册
        /// </summary>
        /// <param name="builder">容器构建器</param>
        public static void Initialize(ContainerBuilder builder)
            => builder.RegisterAssemblyModules(ApplicationPrepare.ApplicationAssemblies.Select(a => a.Assembly).ToArray());
        #endregion
    }
}
