// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Security.Cryptography;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 默认主库主从仓储
/// </summary>
[SuppressSniffer]
public partial class MSRepository : MSRepository<MasterDbContextLocator>, IMSRepository
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IServiceProvider serviceProvider
        , IRepository repository) : base(serviceProvider, repository)
    {
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator> : IMSRepository<TMasterDbContextLocator>
    where TMasterDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IServiceProvider serviceProvider
        , IRepository repository)
    {
        _serviceProvider = serviceProvider;
        _repository = repository;
    }

    /// <summary>
    /// 获取主库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TMasterDbContextLocator>();
    }

    /// <summary>
    /// 动态获取从库（随机）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IPrivateReadableRepository<TEntity> Slave<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        // 判断数据库主库是否注册
        Penetrates.CheckDbContextLocator(typeof(TMasterDbContextLocator), out var dbContextType);

        // 获取主库贴的特性
        var appDbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);

        // 获取从库列表
        var slaveDbContextLocators = appDbContextAttribute.SlaveDbContextLocators;

        // 如果没有定义从库定位器，则抛出异常
        if (slaveDbContextLocators == null || slaveDbContextLocators.Length == 0) throw new InvalidOperationException("Not found slave locators.");

        // 如果只配置了一个从库，直接返回
        if (slaveDbContextLocators.Length == 1) return Slave<TEntity>(() => slaveDbContextLocators[0]);

        // 获取随机从库索引
        var index = RandomNumberGenerator.GetInt32(0, slaveDbContextLocators.Length);

        // 返回随机从库
        return Slave<TEntity>(() => slaveDbContextLocators[index]);
    }

    /// <summary>
    /// 动态获取从库（自定义）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IPrivateReadableRepository<TEntity> Slave<TEntity>(Func<Type> locatorHandler)
        where TEntity : class, IPrivateEntity, new()
    {
        if (locatorHandler == null) throw new ArgumentNullException(nameof(locatorHandler));

        // 获取定位器类型
        var dbContextLocatorType = locatorHandler();
        if (!typeof(IDbContextLocator).IsAssignableFrom(dbContextLocatorType)) throw new InvalidCastException($"{dbContextLocatorType.Name} is not assignable from {nameof(IDbContextLocator)}.");

        // 判断从库定位器是否绑定
        Penetrates.CheckDbContextLocator(dbContextLocatorType, out _);

        // 解析从库定位器
        var repository = _serviceProvider.GetService(typeof(IRepository<,>).MakeGenericType(typeof(TEntity), dbContextLocatorType)) as IPrivateRepository<TEntity>;

        // 返回从库仓储
        return repository.Constraint<IPrivateReadableRepository<TEntity>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1> : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取主库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TMasterDbContextLocator>();
    }

    /// <summary>
    /// 获取从库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator1> Slave1<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator1>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator1>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储2
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator2> Slave2<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator2>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator2>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储3
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator3> Slave3<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator3>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator3>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储4
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator4> Slave4<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator4>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator4>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储5
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator5> Slave5<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator5>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator5>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
    where TSlaveDbContextLocator6 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储6
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator6> Slave6<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator6>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator6>>();
    }
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator7">从库</typeparam>
[SuppressSniffer]
public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
    : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
    , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
    where TSlaveDbContextLocator6 : class, IDbContextLocator
    where TSlaveDbContextLocator7 : class, IDbContextLocator
{
    /// <summary>
    /// 非泛型仓储
    /// </summary>
    private readonly IRepository _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">非泛型仓储</param>
    public MSRepository(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取从库仓储7
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    public virtual IReadableRepository<TEntity, TSlaveDbContextLocator7> Slave7<TEntity>()
        where TEntity : class, IPrivateEntity, new()
    {
        return _repository.Change<TEntity, TSlaveDbContextLocator7>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator7>>();
    }
}