namespace Fur.DependencyInjection
{
    /// <summary>
    /// 注册类型
    /// </summary>
    [SkipScan]
    public enum RegisterType
    {
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient,

        /// <summary>
        /// 作用域
        /// </summary>
        Scoped,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton
    }
}