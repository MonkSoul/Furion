using Fur.Attributes;
using Fur.MirrorController.Options;
using Fur.SwaggerDoc.Options;

namespace Fur.Options
{
    [NonInflated]
    public sealed class AppOptions : IFurOptions
    {
        /// <summary>
        /// 镜像控制器配置选项
        /// </summary>
        public FurMirrorControllerOptions MirrorControllerOptions { get; set; }

        /// <summary>
        /// Swagger配置选项
        /// </summary>
        public FurSwaggerDocOptions SwaggerDocOptions { get; set; }

        /// <summary>
        /// 是否自动配置数据库对象
        /// </summary>
        public bool AutoConfigureDbObjects { get; set; } = true;

        /// <summary>
        /// 项目类型
        /// </summary>
        public FurProjectOptions ProjectType { get; set; } = FurProjectOptions.WebApi;
    }
}