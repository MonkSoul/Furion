// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：Apache-2.0
// 项目地址：https://gitee.com/monksoul/Fur

using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 跳过验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NonValidationAttribute : Attribute
    {
    }
}