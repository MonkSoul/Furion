namespace Fur.DatabaseVisitor.Options
{
    /// <summary>
    /// 数据库表更新选项
    /// </summary>
    public enum DbTableUpdateOptions
    {
        /// <summary>
        /// 全部列更新
        /// </summary>
        AllProperties,

        /// <summary>
        /// 特定列更新
        /// </summary>
        IncludeProperties,

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        ExcludeProperties
    }
}