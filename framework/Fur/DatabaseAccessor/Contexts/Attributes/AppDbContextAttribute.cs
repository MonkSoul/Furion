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

using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AppDbContextAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public AppDbContextAttribute(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 表统一前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 表统一后缀
        /// </summary>
        public string TableSuffix { get; set; }
    }
}