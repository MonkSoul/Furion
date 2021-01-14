using Furion.DependencyInjection;
using System.ComponentModel;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// Json 命名策略选项
    /// </summary>
    [SkipScan]
    public enum JsonNamingPolicyOptions
    {
        /// <summary>
        /// 小写
        /// </summary>
        [Description("首字母小写命名")]
        CamelCase,

        /// <summary>
        /// 保持原有名称定义
        /// </summary>
        [Description("保持原有名称定义")]
        Null
    }
}