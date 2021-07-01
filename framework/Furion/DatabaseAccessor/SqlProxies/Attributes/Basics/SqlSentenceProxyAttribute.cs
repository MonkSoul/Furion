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
using System.Data;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 语句执行代理
    /// </summary>
    [SuppressSniffer]
    public class SqlSentenceProxyAttribute : SqlProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql"></param>
        public SqlSentenceProxyAttribute(string sql)
        {
            Sql = sql;
            CommandType = CommandType.Text;
        }

        /// <summary>
        /// Sql 语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }
    }
}