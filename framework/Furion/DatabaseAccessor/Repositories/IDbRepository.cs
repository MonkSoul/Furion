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
    /// 多数据库仓储
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    public partial interface IDbRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        IRepository<TEntity, TDbContextLocator> Change<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 获取 Sql 操作仓储
        /// </summary>
        /// <returns></returns>
        ISqlRepository<TDbContextLocator> Sql();

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetService<TService>();

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetRequiredService<TService>();
    }
}