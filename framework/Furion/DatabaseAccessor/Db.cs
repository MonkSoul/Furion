// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.EntityFrameworkCore;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库公开类
/// </summary>
[SuppressSniffer]
public static class Db
{
    /// <summary>
    /// 迁移类库名称
    /// </summary>
    internal static string MigrationAssemblyName = "Furion.Database.Migrations";

    /// <summary>
    /// 是否启用自定义租户类型
    /// </summary>
    internal static bool CustomizeMultiTenants;

    /// <summary>
    /// 基于表的多租户外键名
    /// </summary>
    internal static string OnTableTenantId = nameof(Entity.TenantId);

    /// <summary>
    /// 获取非泛型仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IRepository GetRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<IRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取实体仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IRepository{TEntity}</returns>
    public static IRepository<TEntity> GetRepository<TEntity>(IServiceProvider serviceProvider = default)
        where TEntity : class, IPrivateEntity, new()
    {
        return App.GetService<IRepository<TEntity>>(serviceProvider);
    }

    /// <summary>
    /// 获取实体仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IRepository{TEntity, TDbContextLocator}</returns>
    public static IRepository<TEntity, TDbContextLocator> GetRepository<TEntity, TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IRepository<TEntity, TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 根据定位器类型获取仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="dbContextLocator"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IPrivateRepository<TEntity> GetRepository<TEntity>(Type dbContextLocator, IServiceProvider serviceProvider = default)
         where TEntity : class, IPrivateEntity, new()
    {
        return App.GetService(typeof(IRepository<,>).MakeGenericType(typeof(TEntity), dbContextLocator), serviceProvider) as IPrivateRepository<TEntity>;
    }

    /// <summary>
    /// 获取特定数据库上下文仓储
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IDbRepository<TDbContextLocator> GetDbRepository<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IDbRepository<TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取Sql仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static ISqlRepository GetSqlRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<ISqlRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取Sql仓储
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository{TDbContextLocator}</returns>
    public static ISqlRepository<TDbContextLocator> GetSqlRepository<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<ISqlRepository<TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取随机主从库仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static IMSRepository GetMSRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<IMSRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取随机主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IMSRepository{TDbContextLocator}</returns>
    public static IMSRepository<TMasterDbContextLocator> GetMSRepository<TMasterDbContextLocator>(IServiceProvider serviceProvider = default)
        where TMasterDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IMSRepository<TMasterDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取 Sql 代理
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static TSqlDispatchProxy GetSqlProxy<TSqlDispatchProxy>(IServiceProvider serviceProvider = default)
        where TSqlDispatchProxy : class, ISqlDispatchProxy
    {
        return App.GetService<TSqlDispatchProxy>(serviceProvider);
    }

    /// <summary>
    /// 获取默认数据库上下文
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext(IServiceProvider serviceProvider = default)
    {
        return GetDbContext(typeof(MasterDbContextLocator), serviceProvider);
    }

    /// <summary>
    /// 获取特定数据库上下文
    /// </summary>
    /// <param name="dbContextLocator">数据库上下文定位器</param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext(Type dbContextLocator, IServiceProvider serviceProvider = default)
    {
        // 判断数据库上下文定位器是否绑定
        Penetrates.CheckDbContextLocator(dbContextLocator, out _);

        var dbContextResolve = App.GetService<Func<Type, IScoped, DbContext>>(serviceProvider);
        return dbContextResolve(dbContextLocator, default);
    }

    /// <summary>
    /// 获取特定数据库上下文
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return GetDbContext(typeof(TDbContextLocator), serviceProvider);
    }
}