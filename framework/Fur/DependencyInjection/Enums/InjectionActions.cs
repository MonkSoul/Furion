namespace Fur.DependencyInjection
{
    /// <summary>
    /// 服务注册方式
    /// </summary>
    [SkipScan]
    public enum InjectionActions
    {
        /// <summary>
        /// 如果存在则覆盖
        /// </summary>
        Add,

        /// <summary>
        /// 如果存在则跳过，默认方式
        /// </summary>
        TryAdd
    }
}