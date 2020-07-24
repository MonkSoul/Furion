using Fur.ApplicationBase.Attributes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 数据库函数标识器
    /// <para>覆盖 <see cref="Microsoft.EntityFrameworkCore.DbFunctionAttribute"/>，提供数据库上下文标识器功能</para>
    /// <para>说明：只对静态类中的静态方法起作用</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class DbEFFunctionAttribute : DbFunctionAttribute
    {
        #region 构造函数 + private DbFunctionAttribute()
        /// <summary>
        /// 构造函数
        /// <para>私有化，避免未指定名称和schema</para>
        /// </summary>
        private DbEFFunctionAttribute() { }
        #endregion

        #region 构造函数 + public DbFunctionAttribute(string name, string schema) : base(name, schema)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">函数schema</param>
        public DbEFFunctionAttribute(string name, string schema) : base(name, schema) { }
        #endregion

        #region 构造函数 + public DbFunctionAttribute(string name, string schema, params Type[] dbContextIdentifierTypes) : base(name, schema)
        /// <summary>
        /// 构造函数
        /// <para>可以指定数据库上下文标识器类型</para>
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">函数schema</param>
        /// <param name="dbContextIdentifierTypes">数据库上下文标识器类型</param>
        public DbEFFunctionAttribute(string name, string schema, params Type[] dbContextIdentifierTypes) : base(name, schema)
            => DbContextIdentifierTypes = dbContextIdentifierTypes;
        #endregion

        /// <summary>
        /// 数据库上下文标识器类型
        /// </summary>
        public Type[] DbContextIdentifierTypes { get; set; }

        /// <summary>
        /// 私有化基类函数名
        /// </summary>
        private new string Name { get; set; }

        /// <summary>
        /// 私有化基类函数Schema
        /// </summary>
        private new string Schema { get; set; }
    }
}
