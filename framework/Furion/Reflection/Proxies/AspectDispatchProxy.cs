// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;

namespace Furion.Reflection
{
    /// <summary>
    /// 异步代理分发类
    /// </summary>
    public abstract class AspectDispatchProxy
    {
        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProxy"></typeparam>
        /// <returns></returns>
        public static T Create<T, TProxy>() where TProxy : AspectDispatchProxy
        {
            return (T)AspectDispatchProxyGenerator.CreateProxyInstance(typeof(TProxy), typeof(T));
        }

        /// <summary>
        /// 执行同步代理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Invoke(MethodInfo method, object[] args);

        /// <summary>
        /// 执行异步代理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract Task InvokeAsync(MethodInfo method, object[] args);

        /// <summary>
        /// 执行异步返回 Task{T} 代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args);
    }
}