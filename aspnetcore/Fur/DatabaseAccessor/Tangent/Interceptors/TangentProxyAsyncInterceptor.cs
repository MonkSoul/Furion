using Autofac;
using Castle.DynamicProxy;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Tangent.Entities;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Tangent.Interceptors
{
    /// <summary>
    /// 切面代理异步拦截器
    /// </summary>
    [NonWrapper]
    internal class TangentProxyAsyncInterceptor : IAsyncInterceptor
    {
        /// <summary>
        /// autofac 生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        #region 构造函数 + public TangentProxyAsyncInterceptor(ILifetimeScope lifetimeScope)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetimeScope">autofac 生命周期对象</param>
        public TangentProxyAsyncInterceptor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        #endregion 构造函数 + public TangentProxyAsyncInterceptor(ILifetimeScope lifetimeScope)

        #region 同步拦截器 + public void InterceptSynchronous(IInvocation invocation)

        /// <summary>
        /// 同步拦截器
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = TangentDbContextUtilities.SynchronousInvoke(invocation, _lifetimeScope);
        }

        #endregion 同步拦截器 + public void InterceptSynchronous(IInvocation invocation)

        #region 异步无返回值拦截器 + public void InterceptAsynchronous(IInvocation invocation)

        /// <summary>
        /// 异步无返回值拦截器
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = Task.FromResult(TangentDbContextUtilities.AsynchronousOfTInvoke<object>(invocation, _lifetimeScope).Result);
        }

        #endregion 异步无返回值拦截器 + public void InterceptAsynchronous(IInvocation invocation)

        #region 异步有返回值拦截器 + public void InterceptAsynchronous<TResult>(IInvocation invocation)

        /// <summary>
        /// 异步有返回值拦截器
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = Task.FromResult(TangentDbContextUtilities.AsynchronousOfTInvoke<TResult>(invocation, _lifetimeScope).Result);
        }

        #endregion 异步有返回值拦截器 + public void InterceptAsynchronous<TResult>(IInvocation invocation)
    }
}