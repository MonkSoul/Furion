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

namespace Furion.FriendlyException
{
    /// <summary>
    /// 异常复写特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IfExceptionAttribute : Attribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IfExceptionAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="args">格式化参数</param>
        public IfExceptionAttribute(object errorCode, params object[] args)
        {
            ErrorCode = errorCode;
            Args = args;
        }

        /// <summary>
        /// 捕获特定异常类型异常（用于全局异常捕获）
        /// </summary>
        /// <param name="exceptionType"></param>
        public IfExceptionAttribute(Type exceptionType)
        {
            ExceptionType = exceptionType;
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public object ErrorCode { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// 私有异常消息
        /// </summary>
        private string _errorMessage;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get => Oops.FormatErrorMessage(_errorMessage, Args);
            set => _errorMessage = value;
        }

        /// <summary>
        /// 格式化参数
        /// </summary>
        public object[] Args { get; set; }
    }
}