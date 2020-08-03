using Fur.AppCore.Attributes;
using Fur.MirrorController.Options;
using Fur.SwaggerDoc.Options;

namespace Fur.AppCore.Options
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
    }
}