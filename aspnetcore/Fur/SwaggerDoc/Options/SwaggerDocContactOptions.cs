using Fur.AppCore.Attributes;

namespace Fur.SwaggerDoc.Options
{
    /// <summary>
    /// Swagger 联系分组联系信息配置选项
    /// </summary>
    [NonInflated]
    public sealed class SwaggerDocContactOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Uri地址
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}