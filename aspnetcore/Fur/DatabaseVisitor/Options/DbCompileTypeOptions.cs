namespace Fur.DatabaseVisitor.Options
{
    /// <summary>
    /// 数据库编译类型选项
    /// </summary>
    public enum DbCompileTypeOptions
    {
        /// <summary>
        /// 存储过程
        /// </summary>
        DbProcedure,

        /// <summary>
        /// 标量函数
        /// </summary>
        DbScalarFunction,

        /// <summary>
        /// 表值函数
        /// </summary>
        DbTableFunction
    }
}