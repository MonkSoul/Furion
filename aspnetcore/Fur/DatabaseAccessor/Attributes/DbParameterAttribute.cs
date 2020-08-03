using Fur.AppCore.Attributes;
using System;
using System.Data;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 标记类属性生成对应 <see cref="Microsoft.Data.SqlClient.SqlParameter"/> 参数
    /// </summary>
    /// <remarks>
    /// <para>支持类实例属性、方法参数中贴此特性</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter), NonInflated]
    public sealed class DbParameterAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="direction">参数方向，参见：<see cref="ParameterDirection"/></param>
        public DbParameterAttribute(ParameterDirection direction)
            => Direction = direction;

        /// <summary>
        /// 生成 <see cref="Microsoft.Data.SqlClient.SqlParameter"/> 参数方向
        /// </summary>
        /// <remarks>
        /// <para>默认值：<see cref="ParameterDirection.Input"/></para>
        /// </remarks>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;

        /// <summary>
        /// 数据库参数类型，默认为自动识别
        /// </summary>
        public SqlDbType? DbType { get; set; }
    }
}