// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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