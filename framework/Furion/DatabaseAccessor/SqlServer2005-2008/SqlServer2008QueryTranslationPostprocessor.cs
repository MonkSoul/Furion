// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Query;

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