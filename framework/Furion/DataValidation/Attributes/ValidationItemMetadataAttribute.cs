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
using System;
using System.Text.RegularExpressions;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证项元数据
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Field)]
    public class ValidationItemMetadataAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regularExpression">正则表达式</param>
        /// <param name="defaultErrorMessage">失败提示默认消息</param>
        /// <param name="regexOptions">正则表达式匹配选项</param>
        public ValidationItemMetadataAttribute(string regularExpression, string defaultErrorMessage, RegexOptions regexOptions = RegexOptions.None)
        {
            RegularExpression = regularExpression;
            DefaultErrorMessage = defaultErrorMessage;
            RegexOptions = regexOptions;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegularExpression { get; set; }

        /// <summary>
        /// 默认验证失败类型
        /// </summary>
        public string DefaultErrorMessage { get; set; }

        /// <summary>
        /// 正则表达式选项
        /// </summary>
        public RegexOptions RegexOptions { get; set; }
    }
}