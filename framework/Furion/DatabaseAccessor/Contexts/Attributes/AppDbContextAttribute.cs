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

using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AppDbContextAttribute : Attribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(params Type[] slaveDbContextLocators)
        {
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(string connectionString, params Type[] slaveDbContextLocators)
        {
            ConnectionString = connectionString;
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(string connectionString, string providerName, params Type[] slaveDbContextLocators)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库提供器名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 数据库上下文模式
        /// </summary>
        public DbContextMode Mode { get; set; } = DbContextMode.Cached;

        /// <summary>
        /// 表统一前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 表统一后缀
        /// </summary>
        public string TableSuffix { get; set; }

        /// <summary>
        /// 指定从库定位器
        /// </summary>
        public Type[] SlaveDbContextLocators { get; set; }
    }
}