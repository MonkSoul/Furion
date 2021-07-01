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

using Furion.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Query
{
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
}