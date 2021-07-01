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

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证失败模型
    /// </summary>
    [SuppressSniffer]
    public sealed class ValidateFailedModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="messages"></param>
        public ValidateFailedModel(string field, string[] messages)
        {
            Field = field;
            Messages = messages;
        }

        /// <summary>
        /// 出错字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误列表
        /// </summary>
        public string[] Messages { get; set; }
    }
}