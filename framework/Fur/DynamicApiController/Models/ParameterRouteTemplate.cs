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
using System.Collections.Generic;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 参数路由模板
    /// </summary>
    [SkipScan]
    internal class ParameterRouteTemplate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterRouteTemplate()
        {
            ControllerStartTemplates = new List<string>();
            ControllerEndTemplates = new List<string>();
            ActionStartTemplates = new List<string>();
            ActionEndTemplates = new List<string>();
        }

        /// <summary>
        /// 控制器之前的参数
        /// </summary>
        public IList<string> ControllerStartTemplates { get; set; }

        /// <summary>
        /// 控制器之后的参数
        /// </summary>
        public IList<string> ControllerEndTemplates { get; set; }

        /// <summary>
        /// 行为之前的参数
        /// </summary>
        public IList<string> ActionStartTemplates { get; set; }

        /// <summary>
        /// 行为之后的参数
        /// </summary>
        public IList<string> ActionEndTemplates { get; set; }
    }
}