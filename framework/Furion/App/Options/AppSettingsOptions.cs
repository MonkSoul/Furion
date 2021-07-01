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

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using System;

namespace Furion
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    public sealed class AppSettingsOptions : IConfigurableOptions<AppSettingsOptions>
    {
        /// <summary>
        /// 集成 MiniProfiler 组件
        /// </summary>
        public bool? InjectMiniProfiler { get; set; }

        /// <summary>
        /// 是否启用规范化文档
        /// </summary>
        public bool? InjectSpecificationDocument { get; set; }

        /// <summary>
        /// 是否启用分布式内存缓存
        /// </summary>
        public bool? EnabledDistributedMemoryCache { get; set; }

        /// <summary>
        /// 是否启用引用程序集扫描
        /// </summary>
        public bool? EnabledReferenceAssemblyScan { get; set; }

        /// <summary>
        /// 外部程序集
        /// </summary>
        public string[] ExternalAssemblies { get; set; }

        /// <summary>
        /// 是否打印数据库连接信息到 MiniProfiler 中
        /// </summary>
        public bool? PrintDbConnectionInfo { get; set; }

        /// <summary>
        /// 是否记录 EFCore Sql 执行命令日志
        /// </summary>
        public bool? LogEntityFrameworkCoreSqlExecuteCommand { get; set; }

        /// <summary>
        /// 配置支持的包前缀名
        /// </summary>
        public string[] SupportPackageNamePrefixs { get; set; }

        /// <summary>
        /// 是否启用虚拟文件服务
        /// </summary>
        public bool? EnabledVirtualFileServer { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(AppSettingsOptions options, IConfiguration configuration)
        {
            options.InjectMiniProfiler ??= true;
            options.EnabledDistributedMemoryCache ??= true;
            options.InjectSpecificationDocument ??= true;
            options.EnabledReferenceAssemblyScan ??= false;
            options.ExternalAssemblies ??= Array.Empty<string>();
            options.PrintDbConnectionInfo ??= true;
            options.LogEntityFrameworkCoreSqlExecuteCommand ??= true;
            options.SupportPackageNamePrefixs ??= Array.Empty<string>();
            options.EnabledVirtualFileServer ??= true;
        }
    }
}