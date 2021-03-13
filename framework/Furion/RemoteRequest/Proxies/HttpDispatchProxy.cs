using Furion.DependencyInjection;
using Furion.Reflection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 远程请求实现类
    /// </summary>
    [SkipScan]
    public class HttpDispatchProxy : AspectDispatchProxy, IDispatchProxy
    {
        /// <summary>
        /// 被代理对象
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        public IServiceProvider Services { get; set; }

        /// <summary>
        /// 拦截同步方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override object Invoke(MethodInfo method, object[] args)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 拦截异步无返回方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override Task InvokeAsync(MethodInfo method, object[] args)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 拦截异步带返回方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}