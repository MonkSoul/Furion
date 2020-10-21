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

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 接口描述设置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ApiDescriptionSettingsAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiDescriptionSettingsAttribute() : base()
        {
            SplitCamelCase = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用</param>
        public ApiDescriptionSettingsAttribute(bool enabled) : base()
        {
            base.IgnoreApi = !enabled;
            Enabled = enabled;
            SplitCamelCase = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groups">分组列表</param>
        public ApiDescriptionSettingsAttribute(params string[] groups) : base()
        {
            base.GroupName = string.Join(Penetrates.GroupSeparator, groups);
            Groups = groups;
            SplitCamelCase = true;
        }

        /// <summary>
        /// 自定义名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 保留原有名称
        /// </summary>
        public bool KeepName { get; set; }

        /// <summary>
        /// 切割骆驼命名
        /// </summary>
        public bool SplitCamelCase { get; set; }

        /// <summary>
        /// 保留路由谓词
        /// </summary>
        public bool KeepVerb { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
    }
}