// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：Apache-2.0
// 项目地址：https://gitee.com/monksoul/Fur

using System;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 动态 WebApi特性接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DynamicApiControllerAttribute : Attribute
    {
    }
}