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
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Reflection;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 代理方法元数据
    /// </summary>
    [SuppressSniffer]
    public sealed class SqlProxyMethod
    {
        /// <summary>
        /// 参数模型
        /// </summary>
        public object ParameterModel { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public Type ReturnType { get; internal set; }

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public DbContext Context { get; set; }

        /// <summary>
        /// 是否是异步方法
        /// </summary>
        public bool IsAsync { get; internal set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// 最终 Sql 语句
        /// </summary>
        public string FinalSql { get; set; }

        /// <summary>
        /// 当前执行的方法
        /// </summary>
        public MethodInfo Method { get; internal set; }

        /// <summary>
        /// 传递参数
        /// </summary>
        public object[] Arguments { get; internal set; }

        /// <summary>
        /// 拦截Id
        /// </summary>
        public string InterceptorId { get; internal set; }
    }
}