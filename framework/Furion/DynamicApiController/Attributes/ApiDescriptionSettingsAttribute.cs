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
using Furion.DynamicApiController;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 接口描述设置
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ApiDescriptionSettingsAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiDescriptionSettingsAttribute() : base()
        {
            Order = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用</param>
        public ApiDescriptionSettingsAttribute(bool enabled) : base()
        {
            IgnoreApi = !enabled;
            Order = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groups">分组列表</param>
        public ApiDescriptionSettingsAttribute(params string[] groups) : base()
        {
            GroupName = string.Join(Penetrates.GroupSeparator, groups);
            Groups = groups;
            Order = 0;
        }

        /// <summary>
        /// 自定义名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 保留原有名称（Boolean 类型）
        /// </summary>
        public object KeepName { get; set; }

        /// <summary>
        /// 切割骆驼命名（Boolean 类型）
        /// </summary>
        public object SplitCamelCase { get; set; }

        /// <summary>
        /// 保留路由谓词（Boolean 类型）
        /// </summary>
        public object KeepVerb { get; set; }

        /// <summary>
        /// 小写路由（Boolean 类型）
        /// </summary>
        public object LowercaseRoute { get; set; }

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

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 配置控制器区域（只对控制器有效）
        /// </summary>
        public string Area { get; set; }
    }
}