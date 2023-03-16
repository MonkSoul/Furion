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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
[SuppressSniffer]
public abstract class EntityNotKey : EntityNotKey<MasterDbContextLocator>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2> : PrivateEntityNotKey
    where TDbContextLocator2 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
    where TDbContextLocator6 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
    where TDbContextLocator6 : class, IDbContextLocator
    where TDbContextLocator7 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体依赖基接口
/// </summary>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntityNotKey
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
    where TDbContextLocator6 : class, IDbContextLocator
    where TDbContextLocator7 : class, IDbContextLocator
    where TDbContextLocator8 : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    public EntityNotKey(string name) : base(name)
    {
    }
}

/// <summary>
/// 数据库无键实体基类（禁止外部继承）
/// </summary>
[SuppressSniffer]
public abstract class PrivateEntityNotKey : IPrivateEntityNotKey
{
    /// <summary>
    /// 无键实体名
    /// </summary>
    private readonly string _name;

    /// <summary>
    /// 无键实体 schema
    /// </summary>
    private readonly string _schema;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">数据库中定义名</param>
    /// <param name="schema"></param>
    public PrivateEntityNotKey(string name, string schema = default)
    {
        _name = name;
        _schema = schema;
    }

    /// <summary>
    /// 获取视图名称
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return _name;
    }

    /// <summary>
    ///  数据库中定义的 Schema
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string GetSchema()
    {
        return _schema;
    }
}