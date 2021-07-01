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

using Microsoft.Extensions.Logging;
using System;

namespace Furion.Logging
{
    /// <summary>
    /// 构建字符串日志部分类
    /// </summary>
    public sealed partial class StringLoggerPart
    {
        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="message"></param>
        public StringLoggerPart SetMessage(string message)
        {
            Message = message;
            return this;
        }

        /// <summary>
        /// 设置日志级别
        /// </summary>
        /// <param name="level"></param>
        public StringLoggerPart SetLevel(LogLevel level)
        {
            Level = level;
            return this;
        }

        /// <summary>
        /// 设置消息格式化参数
        /// </summary>
        /// <param name="args"></param>
        public StringLoggerPart SetArgs(params object[] args)
        {
            Args = args;
            return this;
        }

        /// <summary>
        /// 设置事件 Id
        /// </summary>
        /// <param name="eventId"></param>
        public StringLoggerPart SetEventId(EventId eventId)
        {
            EventId = eventId;
            return this;
        }

        /// <summary>
        /// 设置日志分类
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        public StringLoggerPart SetCategory<TClass>()
        {
            CategoryType = typeof(TClass);
            return this;
        }

        /// <summary>
        /// 设置日志分类名
        /// </summary>
        /// <param name="categoryName"></param>
        public StringLoggerPart SetCategory(string categoryName)
        {
            CategoryName = categoryName;
            return this;
        }

        /// <summary>
        /// 设置异常对象
        /// </summary>
        public StringLoggerPart SetException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        /// <summary>
        /// 设置日志服务作用域
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public StringLoggerPart SetLoggerScoped(IServiceProvider serviceProvider)
        {
            LoggerScoped = serviceProvider;
            return this;
        }
    }
}