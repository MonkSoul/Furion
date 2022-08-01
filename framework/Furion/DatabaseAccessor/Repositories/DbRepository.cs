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

using Microsoft.Extensions.DependencyInjection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 多数据库仓储
/// </summary>
/// <typeparam name="TDbContextLocator"></typeparam>
public partial class DbRepository<TDbContextLocator> : IDbRepository<TDbContextLocator>
    where TDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public DbRepository(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 切换实体
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public virtual IRepository<TEntity, TDbContextLocator> Change<TEntity>()
         where TEntity : class, IPrivateEntity, new()
    {
        return _serviceProvider.GetService<IRepository<TEntity, TDbContextLocator>>();
    }

    /// <summary>
    /// 获取 Sql 操作仓储
    /// </summary>
    /// <returns></returns>
    public virtual ISqlRepository<TDbContextLocator> Sql()
    {
        return _serviceProvider.GetService<ISqlRepository<TDbContextLocator>>();
    }

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public virtual TService GetService<TService>()
    {
        return _serviceProvider.GetService<TService>();
    }

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public virtual TService GetRequiredService<TService>()
    {
        return _serviceProvider.GetRequiredService<TService>();
    }
}