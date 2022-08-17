// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 操作仓储接口
/// </summary>
public interface ISqlRepository : ISqlRepository<MasterDbContextLocator>
    , ISqlExecutableRepository
    , ISqlReaderRepository
{
}

/// <summary>
/// Sql 操作仓储接口
/// </summary>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public interface ISqlRepository<TDbContextLocator> : IPrivateSqlRepository
    , ISqlExecutableRepository<TDbContextLocator>
    , ISqlReaderRepository<TDbContextLocator>
   where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 私有 Sql 仓储
/// </summary>
public interface IPrivateSqlRepository : IPrivateSqlExecutableRepository
    , IPrivateSqlReaderRepository
    , IPrivateRootRepository
{
    /// <summary>
    /// 数据库操作对象
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    DbContext Context { get; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
        where TChangeDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetService<TService>()
        where TService : class;

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetRequiredService<TService>()
        where TService : class;

    /// <summary>
    /// 将仓储约束为特定仓储
    /// </summary>
    /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
    /// <returns>TRestrainRepository</returns>
    TRestrainRepository Constraint<TRestrainRepository>()
        where TRestrainRepository : class, IPrivateRootRepository;

    /// <summary>
    /// 确保工作单元（事务）可用
    /// </summary>
    void EnsureTransaction();
}