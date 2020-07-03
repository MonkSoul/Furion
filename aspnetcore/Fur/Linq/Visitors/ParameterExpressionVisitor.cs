using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fur.Linq.Visitors
{
    /// <summary>
    /// 自定义参数表达式访问器
    /// </summary>
    internal class ParameterExpressionVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 参数表达式映射集合
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter;

        #region 构造函数 + public ParameterExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterExpressionSetter">参数表达式映射集合</param>
        public ParameterExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter)
        {
            this.parameterExpressionSetter = parameterExpressionSetter ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        #endregion 构造函数 + public ParameterExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter)

        #region 替换表达式参数 +/* public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter, Expression expression)

        /// <summary>
        /// 替换表达式参数
        /// </summary>
        /// <param name="parameterExpressionSetter">参数表达式映射集合</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter, Expression expression)
            => new ParameterExpressionVisitor(parameterExpressionSetter).Visit(expression);

        #endregion 替换表达式参数 +/* public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter, Expression expression)

        #region 重写基类参数访问器 +- protected override Expression VisitParameter(ParameterExpression parameterExpression)

        /// <summary>
        /// 重写基类参数访问器
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            if (parameterExpressionSetter.TryGetValue(parameterExpression, out ParameterExpression replacement))
            {
                parameterExpression = replacement;
            }

            return base.VisitParameter(parameterExpression);
        }

        #endregion 重写基类参数访问器 +- protected override Expression VisitParameter(ParameterExpression parameterExpression)
    }
}