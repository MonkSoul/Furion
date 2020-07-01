using Fur.DatabaseVisitor.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> : IDisposable where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public DbContext DbContext { get; }
        /// <summary>
        /// 实体对象
        /// </summary>
        public DbSet<TEntity> Entity { get; }
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public DatabaseFacade Database { get; }
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        public DbConnection DbConnection { get; }
    }
}
