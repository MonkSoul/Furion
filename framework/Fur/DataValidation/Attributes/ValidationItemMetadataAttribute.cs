// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证项元数据
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Field)]
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