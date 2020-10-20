// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 代理方法元数据
    /// </summary>
    [SkipScan]
    internal sealed class SqlProxyMethod
    {
        /// <summary>
        /// 参数模型
        /// </summary>
        public object ParameterModel { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public DbContext DbContext { get; set; }

        /// <summary>
        /// 是否是异步方法
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// 最终 Sql 语句
        /// </summary>
        public string FinalSql { get; set; }
    }
}