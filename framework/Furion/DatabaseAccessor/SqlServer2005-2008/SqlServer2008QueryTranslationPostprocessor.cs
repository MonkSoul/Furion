// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Query;

/// <summary>
/// SqlServer 查询转换器
/// </summary>
internal class SqlServer2008QueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
{
#if !NET9_0
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
#else
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dependencies"></param>
    /// <param name="relationalDependencies"></param>
    /// <param name="queryCompilationContext"></param>
    public SqlServer2008QueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, QueryCompilationContext queryCompilationContext)
        : base(dependencies, relationalDependencies, (RelationalQueryCompilationContext)queryCompilationContext)
    {
    }
#endif

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