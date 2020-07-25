namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库无键实体抽象类
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    public interface IDbNoKeyEntity : IDbEntity
    {
        /// <summary>
        /// 数据库定义名称
        /// <para>需包含 schema</para>
        /// <para>之所以这样命名，是避免和类自定义属性冲突</para>
        /// </summary>
        string DB_DEFINED_NAME { get; set; }
    }
}