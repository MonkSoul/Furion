using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发规则匹配结果
    /// </summary>
    [SkipScan]
    public class UrlRewriteMatchResult
    {
        /// <summary>
        /// 是否成功匹配到转发规则
        /// </summary>
        public bool IsMatch { get; set; }

        /// <summary>
        /// 匹配到的规则路由前缀
        /// </summary>
        public PathString Prefix { get; set; }

        /// <summary>
        /// 转发规则中的目的主机地址
        /// </summary>
        public string ToHost { get; set; }
    }
}