using Fur.ApplicationBase.Attributes;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 数据库函数定位器
    /// <para>覆盖 <see cref="Microsoft.EntityFrameworkCore.DbFunctionAttribute"/>，提供数据库上下文定位器功能</para>
    /// <para>说明：只对静态类中的静态方法起作用</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class DbFunctionAttribute : Microsoft.EntityFrameworkCore.DbFunctionAttribute
    {
        #region 构造函数 + public DbFunctionAttribute(string name, string schema)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">函数schema</param>
        public DbFunctionAttribute(string name, string schema)
            : base(name, schema) { }
        #endregion

        #region 构造函数 + public DbFunctionAttribute(string name, string schema, params Type[] dbContextLocators)
        /// <summary>
        /// 构造函数
        /// <para>可以指定数据库上下文定位器类型</para>
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">函数schema</param>
        /// <param name="dbContextLocators">数据库上下文定位器类型</param>
        public DbFunctionAttribute(string name, string schema, params Type[] dbContextLocators)
            : base(name, schema)
            => DbContextLocators = dbContextLocators;
        #endregion

        /// <summary>
        /// 数据库上下文定位器类型
        /// </summary>
        public Type[] DbContextLocators { get; set; }
    }
}
