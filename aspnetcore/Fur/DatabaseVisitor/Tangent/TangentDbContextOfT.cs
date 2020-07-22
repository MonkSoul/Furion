using Autofac;
using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Tangent.Entities;
using Fur.DatabaseVisitor.Tangent.Interceptors;

namespace Fur.DatabaseVisitor.Tangent
{
    /// <summary>
    /// 泛型切面数据库操作上下文
    /// </summary>
    /// <typeparam name="TTangent">切面上下文接口依赖</typeparam>
    public class TangentDbContextOfT<TTangent> : ITangentDbContextOfT<TTangent> where TTangent : class, ITangentProxyDependency
    {
        /// <summary>
        /// autofac 实例对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        #region 构造函数 + public TangentDbContextOfT(ILifetimeScope lifetimeScope)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetimeScope">autofac实例对象</param>
        public TangentDbContextOfT(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        #endregion 构造函数 + public TangentDbContextOfT(ILifetimeScope lifetimeScope)

        /// <summary>
        /// 被代理的接口私有实例
        /// </summary>
        private TTangent _proxy;

        /// <summary>
        /// 被代理的接口对象
        /// </summary>
        public TTangent Proxy
        {
            get
            {
                if (_proxy == null)
                {
                    _proxy = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<TTangent>(new TangentProxyInterceptor(new TangentProxyAsyncInterceptor(_lifetimeScope)));
                }

                return _proxy;
            }
        }
    }
}