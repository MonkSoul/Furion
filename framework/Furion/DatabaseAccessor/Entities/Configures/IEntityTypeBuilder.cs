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