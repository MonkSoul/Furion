// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc2.2020.10.14
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur 
// 				    Github：https://github.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 动态接口控制器特性提供器
    /// </summary>
    [SkipScan]
    public sealed class DynamicApiControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 扫描控制器
        /// </summary>
        /// <param name="typeInfo">类型</param>
        /// <returns>bool</returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return Penetrates.IsController(typeInfo);
        }
    }
}