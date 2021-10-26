// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion;
using Furion.DependencyInjection;
using Furion.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    [SuppressSniffer]
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, string assemblyName = default)
        {
            var frameworkPackageName = assemblyName ?? Reflect.GetAssemblyName(typeof(HostBuilderExtensions));
            //Start 将环境变量合并进去支持通用的ihoststartup
            var hostServiceEnv = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES");
            hostServiceEnv = frameworkPackageName + ";" + hostServiceEnv;
            var hostServiceArr = hostServiceEnv.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            hostServiceEnv = hostServiceArr.Distinct().Aggregate((a, b) => a + ";" + b);
            
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, hostServiceEnv);
            //End 将环境变量合并进去支持通用的ihoststartup
            return hostBuilder;
        }

        /// <summary>
        /// 泛型主机注入
        /// </summary>
        /// <param name="hostBuilder">泛型主机注入构建器</param>
        /// <param name="autoRegisterBackgroundService">是否自动注册 BackgroundService</param>
        /// <returns>IWebHostBuilder</returns>
        public static IHostBuilder Inject(this IHostBuilder hostBuilder, bool autoRegisterBackgroundService = true)
        {
            InternalApp.ConfigureApplication(hostBuilder, autoRegisterBackgroundService);

            return hostBuilder;
        }
    }
}
