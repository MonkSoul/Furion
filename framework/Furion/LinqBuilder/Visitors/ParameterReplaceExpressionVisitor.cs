// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Linq.Expressions;

namespace Furion.LinqBuilder;

/// <summary>
/// 处理 Lambda 参数不一致问题
/// </summary>
internal sealed class ParameterReplaceExpressionVisitor : ExpressionVisitor
{
    /// <summary>
    /// 参数表达式映射集合
    /// </summary>
    private readonly Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="parameterExpressionSetter">参数表达式映射集合</param>
    public ParameterReplaceExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter)
    {
        this.parameterExpressionSetter = parameterExpressionSetter ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    /// <summary>
    /// 替换表达式参数
    /// </summary>
    /// <param name="parameterExpressionSetter">参数表达式映射集合</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的表达式</returns>
    public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter, Expression expression)
    {
        return new ParameterReplaceExpressionVisitor(parameterExpressionSetter).Visit(expression);
    }

    /// <summary>
    /// 重写基类参数访问器
    /// </summary>
    /// <param name="parameterExpression"></param>
    /// <returns></returns>
    protected override Expression VisitParameter(ParameterExpression parameterExpression)
    {
        if (parameterExpressionSetter.TryGetValue(parameterExpression, out var replacement))
        {
            parameterExpression = replacement;
        }

        return base.VisitParameter(parameterExpression);
    }
}