using Fur.Attributes;

namespace Fur.SwaggerDoc.Options
{
    /// <summary>
    /// Swagger 分组配置选项
    /// </summary>
    [NonInflated]
    public sealed class SwaggerDocGroupOptions
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 接口协议信息
        /// </summary>
        public string TermsOfService { get; set; }

        /// <summary>
        /// 联系信息
        /// </summary>
        public SwaggerDocContactOptions Contact { get; set; }

        /// <summary>
        /// 协议
        /// </summary>
        public SwaggerDocLicenseOptions License { get; set; }
    }
}