// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 实体执行部件
/// </summary>
public sealed partial class EntityExecutePart<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 设置实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public EntityExecutePart<TEntity> SetEntity(TEntity entity)
    {
        Entity = entity;
        return this;
    }

    /// <summary>
    /// 设置数据库执行作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public EntityExecutePart<TEntity> SetContextScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) ContextScoped = serviceProvider;
        return this;
    }

    /// <summary>
    /// 设置数据库上下文定位器
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    /// <returns></returns>
    public EntityExecutePart<TEntity> Change<TDbContextLocator>()
        where TDbContextLocator : class, IDbContextLocator
    {
        return Change(typeof(TDbContextLocator));
    }

    /// <summary>
    /// 设置数据库上下文定位器
    /// </summary>
    /// <returns></returns>
    public EntityExecutePart<TEntity> Change(Type dbContextLocator)
    {
        if (dbContextLocator != null) DbContextLocator = dbContextLocator;
        return this;
    }
}