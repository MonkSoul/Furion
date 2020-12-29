using System.Threading.Tasks;

namespace System.Reflection
{
    /// <summary>
    /// 异步代理分发类
    /// </summary>
    public abstract class DispatchProxyAsync
    {
        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProxy"></typeparam>
        /// <returns></returns>
        public static T Create<T, TProxy>() where TProxy : DispatchProxyAsync
        {
            return (T)AsyncDispatchProxyGenerator.CreateProxyInstance(typeof(TProxy), typeof(T));
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