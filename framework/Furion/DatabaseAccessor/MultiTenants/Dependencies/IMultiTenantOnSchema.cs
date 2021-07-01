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

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 基于数据库架构的多租户模式
    /// </summary>
    public interface IMultiTenantOnSchema : IPrivateMultiTenant
    {
        /// <summary>
        /// 获取数据库架构名称
        /// </summary>
        /// <returns></returns>
        string GetSchemaName();
    }
}