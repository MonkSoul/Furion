using Autofac;
using Castle.DynamicProxy;
using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Tangent.Utilities;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Tangent.Interceptors
{
    /// <summary>
    /// 切面代理异步拦截器
    /// </summary>
    [NonInflated]
    internal class TangentProxyAsyncInterceptor : IAsyncInterceptor
    {
        /// <summary>
        /// autofac 生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetimeScope">autofac 生命周期对象</param>
        public TangentProxyAsyncInterceptor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 同步拦截器
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = TangentDbContextUtilities.SynchronousInvoke(invocation, _lifetimeScope);
        }

        /// <summary>
        /// 异步无返回值拦截器
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = Task.FromResult(TangentDbContextUtilities.AsynchronousInvoke<object>(invocation, _lifetimeScope).Result);
        }

        /// <summary>
        /// 异步有返回值拦截器
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = Task.FromResult(TangentDbContextUtilities.AsynchronousInvoke<TResult>(invocation, _lifetimeScope).Result);
        }
    }
}