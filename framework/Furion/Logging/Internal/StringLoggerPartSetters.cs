// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.Templates.Extensions;
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
            // 支持读取配置渲染
            if (message != null) Message = message.Render();
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
            if (args != null && args.Length > 0) Args = args;
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
            if (!string.IsNullOrWhiteSpace(categoryName)) CategoryName = categoryName;
            return this;
        }

        /// <summary>
        /// 设置异常对象
        /// </summary>
        public StringLoggerPart SetException(Exception exception)
        {
            if (exception != null) Exception = exception;
            return this;
        }

        /// <summary>
        /// 设置日志服务作用域
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public StringLoggerPart SetLoggerScoped(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null) LoggerScoped = serviceProvider;
            return this;
        }
    }
}