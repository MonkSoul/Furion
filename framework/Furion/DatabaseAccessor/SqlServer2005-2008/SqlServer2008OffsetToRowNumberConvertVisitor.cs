// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Query;

/// <summary>
/// 处理 .Skip().Take() 表达式问题
/// </summary>
internal class SqlServer2008OffsetToRowNumberConvertVisitor : ExpressionVisitor
{
    /// <summary>
    /// 筛选列访问器
    /// </summary>
    private static readonly MethodInfo GenerateOuterColumnAccessor;

    /// <summary>
    /// 引用 TableReferenceExpression 类型
    /// </summary>
    private static readonly Type TableReferenceExpressionType;

    /// <summary>
    /// 表达式根节点
    /// </summary>
    private readonly Expression root;

    /// <summary>
    /// Sql 表达式工厂
    /// </summary>
    private readonly ISqlExpressionFactory sqlExpressionFactory;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static SqlServer2008OffsetToRowNumberConvertVisitor()
    {
        var method = typeof(SelectExpression).GetMethod("GenerateOuterColumn", BindingFlags.NonPublic | BindingFlags.Instance);

        if (!typeof(ColumnExpression).IsAssignableFrom(method?.ReturnType))
            throw new InvalidOperationException("SelectExpression.GenerateOuterColumn() is not found.");

        TableReferenceExpressionType = method.GetParameters().First().ParameterType;
        GenerateOuterColumnAccessor = method;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="root"></param>
    /// <param name="sqlExpressionFactory"></param>
    public SqlServer2008OffsetToRowNumberConvertVisitor(Expression root, ISqlExpressionFactory sqlExpressionFactory)
    {
        this.root = root;
        this.sqlExpressionFactory = sqlExpressionFactory;
    }

    /// <summary>
    /// 替换表达式
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitExtension(Expression node)
    {
        if (node is ShapedQueryExpression shapedQueryExpression)
        {
            return shapedQueryExpression.Update(Visit(shapedQueryExpression.QueryExpression), shapedQueryExpression.ShaperExpression);
        }
        if (node is SelectExpression se)
            node = VisitSelect(se);
        return base.VisitExtension(node);
    }

    /// <summary>
    /// 更新 Select 语句
    /// </summary>
    /// <param name="selectExpression"></param>
    /// <returns></returns>
    private Expression VisitSelect(SelectExpression selectExpression)
    {
        var oldOffset = selectExpression.Offset;
        if (oldOffset == null)
            return selectExpression;

        var oldLimit = selectExpression.Limit;
        var oldOrderings = selectExpression.Orderings;

        // 在子查询中 OrderBy 必须写 Top 数量
        var newOrderings = oldOrderings.Count > 0 && (oldLimit != null || selectExpression == root)
            ? oldOrderings.ToList()
            : new List<OrderingExpression>();

#if NET9_0_OR_GREATER
        // 更新表达式
        selectExpression = selectExpression.Update(selectExpression.Tables.ToList(),
                                                   selectExpression.Predicate,
                                                   selectExpression.GroupBy.ToList(),
                                                   selectExpression.Having,
                                                   selectExpression.Projection.ToList(),
                                                   orderings: newOrderings,
                                                   limit: null,
                                                   offset: null);
#else
        // 更新表达式
        selectExpression = selectExpression.Update(selectExpression.Projection.ToList(),
                                                   selectExpression.Tables.ToList(),
                                                   selectExpression.Predicate,
                                                   selectExpression.GroupBy.ToList(),
                                                   selectExpression.Having,
                                                   orderings: newOrderings,
                                                   limit: null,
                                                   offset: null);
#endif
        var rowOrderings = oldOrderings.Count != 0 ? oldOrderings
            : new[] { new OrderingExpression(new SqlFragmentExpression("(SELECT 1)"), true) };

        selectExpression.PushdownIntoSubquery();

        var subQuery = (SelectExpression)selectExpression.Tables[0];
        var projection = new RowNumberExpression(Array.Empty<SqlExpression>(), rowOrderings, oldOffset.TypeMapping);
        var left = GenerateOuterColumnAccessor.Invoke(subQuery
            , new object[]
            {
                Activator.CreateInstance(TableReferenceExpressionType, new object[] { subQuery,subQuery.Alias }),
                projection,
                "row",
                true
            }) as ColumnExpression;

        selectExpression.ApplyPredicate(sqlExpressionFactory.GreaterThan(left, oldOffset));

        if (oldLimit != null)
        {
            if (oldOrderings.Count == 0)
            {
                selectExpression.ApplyPredicate(sqlExpressionFactory.LessThanOrEqual(left, sqlExpressionFactory.Add(oldOffset, oldLimit)));
            }
            else
            {
                // 这里不支持子查询的 OrderBy 操作
                selectExpression.ApplyLimit(oldLimit);
            }
        }
        return selectExpression;
    }
}