using Fur.DependencyInjection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库函数类型
    /// </summary>
    [SkipScan]
    internal enum DbFunctionType
    {
        /// <summary>
        /// 标量函数
        /// </summary>
        Scalar,

        /// <summary>
        /// 表值函数
        /// </summary>
        Table
    }
}