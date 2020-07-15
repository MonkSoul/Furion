using Fur.MirrorController.Options;
using Fur.SwaggerGen.Options;

namespace Fur.ApplicationBase.Options
{
    /// <summary>
    /// 全局配置总选项
    /// </summary>
    public sealed class FurOptions
    {
        public MirrorControllerOptions MirrorControllerOptions { get; set; }
        public SwaggerOptions swaggerOptions { get; set; }
    }
}