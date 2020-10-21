// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    public interface IEntityNotKey : IEntityNotKey<MasterDbContextLocator>
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    public interface IEntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IPrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
        where TDbContextLocator8 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 无键实体基接口（禁止外部直接继承）
    /// </summary>
    public interface IPrivateEntityNotKey : IPrivateEntity
    {
        /// <summary>
        /// 数据库中定义名
        /// </summary>
        string GetName();
    }
}