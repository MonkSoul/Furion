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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IEntityTypeBuilder<TEntity> : IEntityTypeBuilder<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
    where TDbContextLocator6 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
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
/// 数据库实体类型配置依赖接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IPrivateEntityTypeBuilder<TEntity>
    where TEntity : class, IPrivateEntity, new()
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
/// 数据库实体类型配置依赖接口（禁止外部继承）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IPrivateEntityTypeBuilder<TEntity> : IPrivateModelBuilder
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 实体类型配置
    /// </summary>
    /// <param name="entityBuilder">实体类型构建器</param>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="dbContextLocator">数据库上下文定位器</param>
    void Configure(EntityTypeBuilder<TEntity> entityBuilder, DbContext dbContext, Type dbContextLocator);
}