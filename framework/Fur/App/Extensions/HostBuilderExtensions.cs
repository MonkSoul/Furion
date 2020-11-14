using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    [SkipScan]
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, string assemblyName = nameof(Fur))
        {
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, assemblyName);
            return hostBuilder;
        }
    }
}