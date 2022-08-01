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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库实体依赖基类
/// </summary>
[SuppressSniffer]
public abstract class EntityBase : EntityBase<int, MasterDbContextLocator>
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey> : EntityBase<TKey, MasterDbContextLocator>
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : PrivateEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    where TDbContextLocator3 : class, IDbContextLocator
    where TDbContextLocator4 : class, IDbContextLocator
    where TDbContextLocator5 : class, IDbContextLocator
    where TDbContextLocator6 : class, IDbContextLocator
{
}

/// <summary>
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityBase<TKey>
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
/// 数据库实体依赖基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
/// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
[SuppressSniffer]
public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntityBase<TKey>
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
/// 数据库实体依赖基类（禁止外部继承）
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
[SuppressSniffer]
public abstract class PrivateEntityBase<TKey> : IPrivateEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey Id { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Guid? TenantId { get; set; }
}