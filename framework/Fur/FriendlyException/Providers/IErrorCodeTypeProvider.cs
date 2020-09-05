// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：Apache-2.0
// 项目地址：https://gitee.com/monksoul/Fur

using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常错误代码提供器
    /// </summary>
    public interface IErrorCodeTypeProvider
    {
        /// <summary>
        /// 错误代码定义类型
        /// </summary>
        Type[] Definitions { get; }
    }
}