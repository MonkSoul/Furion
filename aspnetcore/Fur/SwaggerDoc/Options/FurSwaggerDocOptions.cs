using Fur.AppCore.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fur.SwaggerDoc.Options
{
    /// <summary>
    /// Swagger 文档配置选项
    /// </summary>
    [NonInflated]
    public sealed class FurSwaggerDocOptions
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// 加载注释的程序集
        /// </summary>
        public string[] LoadCommentsAssemblies { get; set; }

        /// <summary>
        /// 所有分组配置选项
        /// </summary>
        public SwaggerDocGroupOptions[] Groups { get; set; }

        /// <summary>
        /// 启用MiniProfiler监听组件
        /// </summary>
        [Required]
        public bool EnableMiniProfiler { get; set; }

        /// <summary>
        /// 默认分组名
        /// </summary>
        [Required]
        public string DefaultGroupName { get; set; }
    }
}