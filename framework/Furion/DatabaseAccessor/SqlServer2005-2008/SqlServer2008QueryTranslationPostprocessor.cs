using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    /// SqlServer 查询转换器
    /// </summary>
    internal class SqlServer2008QueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="queryCompilationContext"></param>
        public SqlServer2008QueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        /// <summary>
        /// 替换分页语句
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override Expression Process(Expression query)
        {
            query = base.Process(query);
            query = new SqlServer2008OffsetToRowNumberConvertVisitor(query, RelationalDependencies.SqlExpressionFactory).Visit(query);
            return query;
        }
    }
}