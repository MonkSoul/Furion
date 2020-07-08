using System;
using System.Data;

namespace Fur.DatabaseVisitor.Attributes
{
    /// <summary>
    /// 生成数据库执行参数特性类
    /// <para>主要用于将 Model 转 <see cref="Microsoft.Data.SqlClient.SqlParameter"/> 类</para>
    /// <para>只能贴到属性上</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttribute : Attribute
    {
        #region 默认构造函数 + public DbParameterAttribute(string name) => Name = name; 
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="name">数据库参数名</param>
        public DbParameterAttribute(string name) => Name = name;
        #endregion

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
    }
}