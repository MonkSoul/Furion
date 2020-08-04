using Castle.DynamicProxy;

namespace Fur.DatabaseAccessor.Interceptors
{
    /// <summary>
    /// 切面代理同步拦截器
    /// </summary>

    internal class TangentProxyInterceptor : IInterceptor
    {
        /// <summary>
        /// 异步拦截器
        /// </summary>
        private readonly TangentProxyAsyncInterceptor _tangentProxyAsyncInterceptor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tangentProxyAsyncInterceptor">异步拦截器</param>
        public TangentProxyInterceptor(TangentProxyAsyncInterceptor tangentProxyAsyncInterceptor)
        {
            _tangentProxyAsyncInterceptor = tangentProxyAsyncInterceptor;
        }

        /// <summary>
        /// 拦截具体方法
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void Intercept(IInvocation invocation)
        {
            _tangentProxyAsyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}