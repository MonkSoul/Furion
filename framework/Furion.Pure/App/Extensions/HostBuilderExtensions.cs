// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion;
using Furion.Extensions;
using Furion.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.Hosting;

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
        // 获取默认程序集名称
        var defaultAssemblyName = assemblyName ?? Reflect.GetAssemblyName(typeof(HostBuilderExtensions));

        //  获取环境变量 ASPNETCORE_HOSTINGSTARTUPASSEMBLIES 配置
        var environmentVariables = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES");
        var combineAssembliesName = $"{defaultAssemblyName};{environmentVariables}".ClearStringAffixes(1, ";");

        hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, combineAssembliesName);

        // 实现假的 Starup，解决泛型主机启动问题
        hostBuilder.UseStartup<FakeStartup>();
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