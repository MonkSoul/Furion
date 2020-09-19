// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译异常类
    /// </summary>
    [NonBeScan]
    public class ViewEngineCompilationException : ViewEngineException
    {
        public ViewEngineCompilationException()
        {
        }

        protected ViewEngineCompilationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ViewEngineCompilationException(string message) : base(message)
        {
        }

        public ViewEngineCompilationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public List<Diagnostic> Errors { get; set; }
        public string GeneratedCode { get; set; }
    }
}