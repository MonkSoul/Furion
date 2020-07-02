using System.ComponentModel.DataAnnotations;

namespace Fur.SwaggerGen.Options
{
    /// <summary>
    /// Swagger 文档配置选项
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle { get; set; }
        /// <summary>
        /// 所有分组配置选项
        /// </summary>
        public SwaggerGroupOptions[] Groups { get; set; }
        /// <summary>
        /// 启用MiniProfiler监听组件
        /// </summary>
        [Required]
        public bool EnableMiniProfiler { get; set; }
    }
}
