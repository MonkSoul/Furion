// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 参数路由模板
    /// </summary>
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