using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 实体函数配置特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class QueryableFunctionAttribute : DbFunctionAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">架构名</param>
        public QueryableFunctionAttribute(string name, string schema = null) : base(name, schema)
        {
            DbContextLocators = Array.Empty<Type>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">架构名</param>
        /// <param name="dbContextLocators">数据库上下文定位器</param>
        public QueryableFunctionAttribute(string name, string schema = null, params Type[] dbContextLocators) : base(name, schema)
        {
            DbContextLocators = dbContextLocators ?? Array.Empty<Type>();
        }

        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type[] DbContextLocators { get; set; }
    }
}