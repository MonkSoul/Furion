using System;
using System.Data;

namespace Fur.DatabaseVisitor.Attributes
{
    /// <summary>
    /// 生成存储过程参数
    /// <para>主要用于将 Model 转 <see cref="Microsoft.Data.SqlClient.SqlParameter"/> 类</para>
    /// <para>只能贴到属性上</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbProcedureParameterAttribute : Attribute
    {
        #region 构造函数 +  public DbParameterAttribute(ParameterDirection direction)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="direction">参数方向 <see cref="ParameterDirection"/></param>
        public DbProcedureParameterAttribute(ParameterDirection direction) => Direction = direction;

        #endregion 构造函数 +  public DbParameterAttribute(ParameterDirection direction)

        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
    }
}