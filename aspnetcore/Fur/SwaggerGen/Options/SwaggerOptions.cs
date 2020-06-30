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
        public string DocumentTime { get; set; }
        /// <summary>
        /// 所有分组配置选项
        /// </summary>
        public SwaggerGroupOptions[] Groups { get; set; }
    }
}
