using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 非泛型仓储接口
    /// <para>区域于泛型仓储接口，非泛型仓储接口无需每个实体进行泛型初始化</para>
    /// </summary>
    public partial interface IRepository
    {
        #region 获取泛型仓储接口 + IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()

        /// <summary>
        /// 获取泛型仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity}"/></returns>
        IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntityBase, new();

        #endregion 获取泛型仓储接口 + IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
    }
}