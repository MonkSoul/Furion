using Fur.Attributes;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// Linq中使用数据库函数特性类
    /// </summary>
    /// <remarks>
    /// <para>覆盖 <see cref="Microsoft.EntityFrameworkCore.DbFunctionAttribute"/>，提供数据库上下文定位器功能</para>
    /// <para>该特性只对静态类中的静态方法有效</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method), NonInflated]
    public sealed class DbFunctionAttribute : Microsoft.EntityFrameworkCore.DbFunctionAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">数据库架构名</param>
        public DbFunctionAttribute(string name, string schema)
            : base(name, schema) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// <para>可以指定数据库上下文定位器类型</para>
        /// </remarks>
        /// <param name="name">函数名</param>
        /// <param name="schema">数据库架构名</param>
        /// <param name="dbContextLocators">数据库上下文定位器</param>
        public DbFunctionAttribute(string name, string schema, params Type[] dbContextLocators)
            : base(name, schema)
            => DbContextLocators = dbContextLocators;

        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type[] DbContextLocators { get; set; }
    }
}