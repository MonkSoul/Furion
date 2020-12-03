using System.ComponentModel;

namespace Furion.DependencyInjection
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
        [Description("瞬时")]
        Transient,

        /// <summary>
        /// 作用域
        /// </summary>
        [Description("作用域")]
        Scoped,

        /// <summary>
        /// 单例
        /// </summary>
        [Description("单例")]
        Singleton
    }
}