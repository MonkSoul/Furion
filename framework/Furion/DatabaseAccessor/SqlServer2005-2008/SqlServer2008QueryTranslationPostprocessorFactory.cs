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

namespace Microsoft.EntityFrameworkCore.Query;

/// <summary>
/// SqlServer 查询转换工厂（处理 SqlServer 2008 分页问题）
/// </summary>
[SuppressSniffer]
public class SqlServer2008QueryTranslationPostprocessorFactory : IQueryTranslationPostprocessorFactory
{
    /// <summary>
    /// 查询转换依赖集合
    /// </summary>
    private readonly QueryTranslationPostprocessorDependencies _dependencies;

    /// <summary>
    /// 关系查询转换依赖集合
    /// </summary>
    private readonly RelationalQueryTranslationPostprocessorDependencies _relationalDependencies;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dependencies"></param>
    /// <param name="relationalDependencies"></param>
    public SqlServer2008QueryTranslationPostprocessorFactory(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies)
    {
        _dependencies = dependencies;
        _relationalDependencies = relationalDependencies;
    }

    /// <summary>
    /// 创建查询转换实例工厂
    /// </summary>
    /// <param name="queryCompilationContext"></param>
    /// <returns></returns>
    public virtual QueryTranslationPostprocessor Create(QueryCompilationContext queryCompilationContext)
    {
        return new SqlServer2008QueryTranslationPostprocessor(
              _dependencies,
              _relationalDependencies,
              queryCompilationContext);
    }
}