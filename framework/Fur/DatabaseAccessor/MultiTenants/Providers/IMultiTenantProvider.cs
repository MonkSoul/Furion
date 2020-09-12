// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 多租户提供器依赖接口
    /// </summary>
    public interface IMultiTenantProvider
    {
        /// <summary>
        /// 基于数据库表的多租户模式
        /// </summary>
        /// <returns></returns>
        Guid? OnTableTenantId();

        /// <summary>
        /// 基于数据库架构名的多租户模式
        /// </summary>
        /// <returns></returns>
        string OnSchemaName();

        /// <summary>
        /// 基于数据库的多租户模式
        /// </summary>
        /// <returns></returns>
        string OnDatabaseConnectionString();
    }
}