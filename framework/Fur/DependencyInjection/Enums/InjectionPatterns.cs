namespace Fur.DependencyInjection
{
    /// <summary>
    /// 注册范围
    /// </summary>
    [SkipScan]
    public enum InjectionPatterns
    {
        /// <summary>
        /// 只注册自己
        /// </summary>
        Self,

        /// <summary>
        /// 第一个接口，默认值
        /// </summary>
        FirstInterface,

        /// <summary>
        /// 自己和第一个接口，默认值
        /// </summary>
        SelfWithFirstInterface,

        /// <summary>
        /// 所有接口
        /// </summary>
        ImplementedInterfaces,

        /// <summary>
        /// 注册自己包括所有接口
        /// </summary>
        All
    }
}