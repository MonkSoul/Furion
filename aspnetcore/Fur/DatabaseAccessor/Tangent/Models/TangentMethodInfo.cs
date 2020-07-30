using Fur.AppBasic.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Fur.DatabaseAccessor.Tangent.Models
{
    /// <summary>
    /// 切面方法信息类
    /// </summary>
    [NonWrapper]
    internal class TangentMethodInfo
    {
        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// 命令参数
        /// </summary>
        public SqlParameter[] SqlParameters { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 实际方法返回值
        /// </summary>
        public Type ActReturnType { get; set; }

        /// <summary>
        /// 有原类型
        /// <para>也就是配置了 SourceType 属性，用来做映射的</para>
        /// </summary>
        public bool HasSourceType { get; set; }

        /// <summary>
        /// 是否元组类型返回值
        /// </summary>
        public bool IsValueTupleReturnType { get; set; }

        /// <summary>
        /// 元组类型泛型参数
        /// </summary>
        public Type[] ValueTupleGenericTypeArguments { get; set; }

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public DbContext DbContext { get; set; }
    }
}