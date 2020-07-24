namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 无键实体配置器
    /// <para>通常指的是存储过程返回值、视图返回值、函数返回值</para>
    /// </summary>
    public interface IDbNoKeyEntity : IDbEntity
    {
        /// <summary>
        /// 实体名称
        /// <para>通常指的是存储过程名、视图名、函数名</para>
        /// <para>之所以这样命名，是避免和类自定义属性冲突</para>
        /// </summary>
        string ENTITY_NAME { get; set; }
    }
}