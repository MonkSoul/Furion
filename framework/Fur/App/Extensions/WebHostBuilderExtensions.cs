// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// Web 主机构建器拓展类
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入StartupFilter 和 HostStartup
        /// </summary>
        /// <param name="webBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder webBuilder, string assemblyName = nameof(Fur))
        {
            webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, assemblyName);
            return webBuilder;
        }
    }
}