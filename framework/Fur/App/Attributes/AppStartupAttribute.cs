using Fur.DependencyInjection;
using System;

namespace Fur
{
    /// <summary>
    /// 注册服务启动配置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public class AppStartupAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public AppStartupAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
    }
}