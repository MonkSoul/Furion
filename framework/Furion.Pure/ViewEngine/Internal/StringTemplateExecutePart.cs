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
using System;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 字符串模板执行部件
    /// </summary>
    [SuppressSniffer]
    public sealed partial class StringTemplateExecutePart
    {
        /// <summary>
        /// 字符串模板
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// 视图配置选项
        /// </summary>
        public Action<IViewEngineOptionsBuilder> TemplateOptionsBuilder { get; private set; }

        /// <summary>
        /// 模型数据
        /// </summary>
        public (Type Type, object Model) TemplateModel { get; private set; } = (typeof(object), default);

        /// <summary>
        /// 模板缓存名称（不含拓展名）
        /// </summary>
        public string TemplateCachedFileName { get; private set; }

        /// <summary>
        /// 视图模板服务作用域
        /// </summary>
        public IServiceProvider ViewEngineScoped { get; private set; }
    }
}