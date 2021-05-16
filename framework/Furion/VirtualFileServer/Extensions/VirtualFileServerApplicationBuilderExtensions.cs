using Furion.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 虚拟文件服务中间件
    /// </summary>
    [SkipScan]
    public static class VirtualFileServerApplicationBuilderExtensions
    {
        /// <summary>
        /// 虚拟文件系统中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseVirtualFileServer(this IApplicationBuilder app)
        {
            return app;
        }
    }
}