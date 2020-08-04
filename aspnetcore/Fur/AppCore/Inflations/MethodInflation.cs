using Fur.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 方法包装器
    /// </summary>
    [NonInflated]
    internal sealed class MethodInflation
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        internal Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 所在类型
        /// </summary>
        internal Type ThisDeclareType { get; set; }

        /// <summary>
        /// 被包装的方法
        /// </summary>
        internal MethodInfo ThisMethod { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        internal Type ReturnType { get; set; }

        /// <summary>
        /// 是否是静态方法
        /// </summary>
        internal bool IsStaticMethod { get; set; }

        /// <summary>
        /// 子参数集合
        /// </summary>
        internal IEnumerable<ParameterInfo> SubParameters { get; set; }

        /// <summary>
        /// 方法特性集合
        /// </summary>
        internal IEnumerable<Attribute> CustomAttributes { get; set; }

        /// <summary>
        /// 是否是控制器Action方法
        /// </summary>
        /// <remarks>
        /// <para>参见：<see cref="Fur.AppCore.App.IsControllerActionMethod(MethodInfo)"/></para>
        /// </remarks>
        internal bool IsControllerActionMethod { get; set; }

        /// <summary>
        /// Swagger 接口文档 分组名
        /// </summary>
        /// <remarks>
        /// <para>只有 <see cref="IsControllerActionMethod"/> = <c>true</c> 有作用</para>
        /// </remarks>
        internal string[] SwaggerGroups { get; set; }

        /// <summary>
        /// 是否是数据库函数
        /// </summary>
        /// <remarks>
        /// <para>贴了 <see cref="Fur.DatabaseAccessor.Attributes.DbFunctionAttribute"/> 特性的静态方法，且所在的类型也必须是静态的</para>
        /// </remarks>
        internal bool IsDbFunctionMethod { get; set; }
    }
}