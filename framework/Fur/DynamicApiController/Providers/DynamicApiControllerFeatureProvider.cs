// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 动态接口控制器特性提供器
    /// </summary>
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