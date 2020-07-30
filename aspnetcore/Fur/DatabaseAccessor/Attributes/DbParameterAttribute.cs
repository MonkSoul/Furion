using Fur.ApplicationBase.Attributes;
using System;
using System.Data;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 数据库Sql参数特性
    /// <para>参见：<see cref="Microsoft.Data.SqlClient.SqlParameter"/></para>
    /// <para>说明：只能在类属性中贴此特性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property), NonWrapper]
    public class DbParameterAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// <para>支持传入 <see cref="ParameterDirection"/> 参数值</para>
        /// </summary>
        /// <param name="direction">参数方向，参见：<see cref="ParameterDirection"/></param>
        public DbParameterAttribute(ParameterDirection direction)
            => Direction = direction;

        /// <summary>
        /// 参数方向
        /// <para>默认：<see cref="ParameterDirection.Input"/>，参见：<see cref="ParameterDirection"/></para>
        /// </summary>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
    }
}