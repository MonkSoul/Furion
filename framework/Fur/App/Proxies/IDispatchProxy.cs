using System;

namespace Fur
{
    /// <summary>
    /// 代理拦截依赖接口
    /// </summary>
    public interface IDispatchProxy
    {
        /// <summary>
        /// 实例
        /// </summary>
        object Target { get; set; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        IServiceProvider Services { get; set; }
    }
}