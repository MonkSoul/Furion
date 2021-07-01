// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.VirtualFileServer;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 虚拟文件服务服务拓展
    /// </summary>
    [SuppressSniffer]
    public static class VirtualFileServerServiceCollectionExtensions
    {
        /// <summary>
        /// 文件提供器系统服务拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddVirtualFileServer(this IServiceCollection services)
        {
            // 解析文件提供器
            services.AddSingleton(provider =>
            {
                static IFileProvider fileProviderResolve(FileProviderTypes fileProviderTypes, object args)
                {
                    // 根据类型创建对应 提供器
                    IFileProvider fileProvider = fileProviderTypes switch
                    {
                        FileProviderTypes.Embedded => new EmbeddedFileProvider(args as Assembly),
                        FileProviderTypes.Physical => new PhysicalFileProvider(args as string),
                        _ => throw new NotSupportedException()
                    };

                    return fileProvider;
                }

                // 转换成委托
                return (Func<FileProviderTypes, object, IFileProvider>)fileProviderResolve;
            });

            return services;
        }
    }
}