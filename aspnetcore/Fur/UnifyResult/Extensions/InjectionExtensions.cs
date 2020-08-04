using Autofac;

using Fur.UnifyResult.Providers;

namespace Fur.UnifyResult.Extensions
{
    /// <summary>
    /// 数据库访问注册拓展类
    /// </summary>

    public static class InjectionExtensions
    {
        public static ContainerBuilder RegisterUnifyProvider<TUnifyProvider>(this ContainerBuilder builder)
        {
            builder.RegisterType<TUnifyProvider>()
                .As<IUnifyResultProvider>()
                .InstancePerLifetimeScope();
            return builder;
        }
    }
}