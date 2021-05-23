// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.6.5
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion 
//          Github：https://github.com/monksoul/Furion 
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 视图引擎模板编译异常类
    /// </summary>
    [SkipScan]
    public class ViewEngineTemplateException : ViewEngineException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewEngineTemplateException()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ViewEngineTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ViewEngineTemplateException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ViewEngineTemplateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<Diagnostic> Errors { get; set; }

        /// <summary>
        /// 生成的代码
        /// </summary>
        public string GeneratedCode { get; set; }
    }
}