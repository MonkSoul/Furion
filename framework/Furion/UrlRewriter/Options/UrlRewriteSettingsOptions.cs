using Furion.DependencyInjection;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发选项
    /// </summary>
    [SkipScan]
    public class UrlRewriteSettingsOptions
    {
        /// <summary>
        /// 是否启用URL转发规则
        /// </summary>
        public bool UrlRewriteEnable { get; set; }

        /// <summary>
        /// URL转发规则列表
        /// </summary>
        public string[][] UrlRewriteRules { get; set; }
    }
}