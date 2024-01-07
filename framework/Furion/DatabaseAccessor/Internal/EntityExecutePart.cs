// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 实体执行部件
/// </summary>
/// <typeparam name="TEntity"></typeparam>
[SuppressSniffer]
public sealed partial class EntityExecutePart<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 静态缺省 Entity 部件
    /// </summary>
    public static EntityExecutePart<TEntity> Default()
    {
        return new();
    }

    /// <summary>
    /// 实体
    /// </summary>
    public TEntity Entity { get; private set; }

    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);

    /// <summary>
    /// 数据库上下文执行作用域
    /// </summary>
    public IServiceProvider ContextScoped { get; private set; }
}