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
using System.Runtime.Serialization;

// reference：https://github.com/adoconnection/RazorEngineCore
namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图异常类
    /// </summary>
    [SkipScan]
    public class ViewEngineException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewEngineException()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ViewEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ViewEngineException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ViewEngineException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}