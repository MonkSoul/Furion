// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息类型提供器
    /// </summary>
    public interface IValidationMessageTypeProvider
    {
        /// <summary>
        /// 验证消息类型定义
        /// </summary>
        Type[] Definitions { get; }
    }
}