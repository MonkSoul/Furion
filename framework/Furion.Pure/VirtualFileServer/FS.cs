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
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace Furion.VirtualFileServer
{
    /// <summary>
    /// 虚拟文件服务静态类
    /// </summary>
    [SuppressSniffer]
    public static class FS
    {
        /// <summary>
        /// 获取物理文件提供器
        /// </summary>
        /// <param name="root"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IFileProvider GetPhysicalFileProvider(string root, IServiceProvider serviceProvider = default)
        {
            return GetFileProvider(FileProviderTypes.Physical, root, serviceProvider);
        }

        /// <summary>
        /// 获取嵌入资源文件提供器
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IFileProvider GetEmbeddedFileProvider(Assembly assembly, IServiceProvider serviceProvider = default)
        {
            return GetFileProvider(FileProviderTypes.Embedded, assembly, serviceProvider);
        }

        /// <summary>
        /// 文件提供器
        /// </summary>
        /// <param name="fileProviderTypes"></param>
        /// <param name="args"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IFileProvider GetFileProvider(FileProviderTypes fileProviderTypes, object args, IServiceProvider serviceProvider = default)
        {
            var fileProviderResolve = App.GetService<Func<FileProviderTypes, object, IFileProvider>>(serviceProvider ?? App.RootServices);
            return fileProviderResolve(fileProviderTypes, args);
        }
    }
}