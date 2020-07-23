namespace Fur.DatabaseAccessor.Options
{
    /// <summary>
    /// 数据库表更新选项
    /// </summary>
    public enum DbTablePropertyUpdateOptions
    {
        /// <summary>
        /// 全部列更新
        /// </summary>
        All,

        /// <summary>
        /// 特定列更新
        /// </summary>
        Include,

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        Exclude
    }
}