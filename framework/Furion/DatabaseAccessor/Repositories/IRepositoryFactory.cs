// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 仓储工厂接口
/// </summary>
public interface IRepositoryFactory<TEntity> : IRepositoryFactory<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 仓储工厂接口
/// </summary>
public interface IRepositoryFactory<TEntity, TDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 创建实体仓储（需要手动 using）
    /// </summary>
    /// <returns></returns>
    IRepository<TEntity, TDbContextLocator> CreateRepository();
}