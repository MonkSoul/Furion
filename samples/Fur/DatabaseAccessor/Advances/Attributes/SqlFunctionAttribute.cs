using Fur.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 函数特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class SqlFunctionAttribute : SqlObjectProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        public SqlFunctionAttribute(string name) : base(name)
        {
        }
    }
}