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
using Microsoft.Extensions.Logging;
using System;

namespace Furion.Logging
{
    /// <summary>
    /// 构建字符串日志部分类
    /// </summary>
    [SuppressSniffer]
    public sealed partial class StringLoggerPart
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; private set; } = LogLevel.Information;

        /// <summary>
        /// 消息格式化参数
        /// </summary>
        public object[] Args { get; private set; }

        /// <summary>
        /// 事件 Id
        /// </summary>
        public EventId? EventId { get; private set; }

        /// <summary>
        /// 日志分类类型（从依赖注入中解析）
        /// </summary>
        public Type CategoryType { get; private set; } = typeof(System.Running.Logging);

        /// <summary>
        /// 日志分类名（总是创建新的实例）
        /// </summary>
        public string CategoryName { get; private set; }

        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 日志对象所在作用域
        /// </summary>
        public IServiceProvider LoggerScoped { get; private set; }
    }
}