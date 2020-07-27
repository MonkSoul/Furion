using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 数据库表特性类
    /// <para>覆盖 <see cref="TableAttribute"/>，提供数据库上下文标识器功能</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttribute : TableAttribute
    {
        #region 构造函数 + public DbTableAttribute(string name)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库表名称</param>
        public DbTableAttribute(string name)
            : base(name)
        {
        }
        #endregion

        #region 构造函数 + public DbTableAttribute(string name, params Type[] dbContextIdentifierTypes)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库表名称</param>
        /// <param name="dbContextIdentifierTypes">数据库上下文标识器类型</param>
        public DbTableAttribute(string name, params Type[] dbContextIdentifierTypes)
            : base(name)
            => DbContextIdentifierTypes = dbContextIdentifierTypes;
        #endregion

        /// <summary>
        /// 数据库上下文标识器类型
        /// </summary>
        public Type[] DbContextIdentifierTypes { get; set; } = new Type[] { };
    }
}
