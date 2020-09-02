// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IWritableRepository<TEntity> : IInsertableRepository<TEntity>, IUpdateableRepository<TEntity>, IDeletableRepository<TEntity>
       where TEntity : class, IDbEntityBase, new()
    {
    }
}