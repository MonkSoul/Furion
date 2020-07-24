namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 无键实体配置器
    /// <para>通常指的是存储过程返回值、视图返回值、函数返回值</para>
    /// </summary>
    public abstract class DbNoKeyEntity : IDbNoKeyEntity
    {
        #region 构造函数 + public DbNoKeyEntity(string entityName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityName">无键实体名</param>
        public DbNoKeyEntity(string entityName)
            => ENTITY_NAME = entityName;
        #endregion

        /// <summary>
        /// 实体名称
        /// <para>通常指的是存储过程名、视图名、函数名</para>
        /// <para>之所以这样命名，是避免和类自定义属性冲突</para>
        /// </summary>
        public string ENTITY_NAME { get; set; }
    }
}