using Fur.DatabaseAccessor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 实体对象
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        IQueryable<TEntity> DerailEntities { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        DbConnection DbConnection { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        Guid? TenantId { get; }
    }

    /// <summary>
    /// 非泛型仓储接口
    /// <para>区域于泛型仓储接口，非泛型仓储接口无需每个实体进行泛型初始化</para>
    /// </summary>
    public partial interface IRepository
    {
        /// <summary>
        /// 获取泛型仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepository{TEntity}"/></returns>
        IRepository<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntityBase, new();
    }
}