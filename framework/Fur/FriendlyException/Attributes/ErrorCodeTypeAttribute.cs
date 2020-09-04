// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 错误代码类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ErrorCodeTypeAttribute : Attribute
    {
    }
}