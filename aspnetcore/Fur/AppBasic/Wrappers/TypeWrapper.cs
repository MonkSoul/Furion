using Fur.AppBasic.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppBasic.Wrappers
{
    /// <summary>
    /// 类型包装类
    /// <code>sealed</code>
    /// <para>类型包装类，主要用来装载解决方案项目中的类型的常用属性及附加属性，避免重复反射读取</para>
    /// </summary>
    [NonWrapper]
    public sealed class TypeWrapper
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        public Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 被包装的类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 是否是泛型类型
        /// </summary>
        public bool IsGenericType { get; set; }

        /// <summary>
        /// 是否是控制器类型。参见：<see cref="Fur.AppBasic.AppGlobal.IsControllerActionType(MethodInfo)"/>
        /// </summary>
        public bool IsControllerType { get; set; }

        /// <summary>
        /// 是否是静态类型
        /// <code>IsAbstract and IsSealed</code>
        /// </summary>
        public bool IsStaticType { get; set; }

        /// <summary>
        /// 能够被 <c>new</c> 关键字创建的类型
        /// </summary>
        public bool CanBeNew { get; set; }

        /// <summary>
        /// Swagger 接口文档 分组名
        /// <para>生成 Swagger 接口文档时，用于匹配特定组信息。</para>
        /// <para>参见：<see cref="Fur.MirrorController.Attributes.MirrorControllerAttribute.SwaggerGroups"/></para>
        /// </summary>
        public string[] SwaggerGroups { get; set; }

        /// <summary>
        /// 泛型类型中的泛型参数
        /// <para>只有 <see cref="IsGenericType"/> = <c>true</c> 有作用</para>
        /// </summary>
        public IEnumerable<Type> GenericArgumentTypes { get; set; }

        /// <summary>
        /// 当前类型下的所有公开属性包装类集合。参见：<see cref="PropertyWrapper"/>
        /// <para>包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>实例属性、静态属性</description>
        /// </item>
        /// </list>
        /// <para>不包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>私有属性</description>
        /// </item>
        /// </list>
        /// </summary>
        public IEnumerable<PropertyWrapper> PublicPropertis { get; set; }

        /// <summary>
        /// 当前类型下的所有公开方法包装类集合。参见：<see cref="MethodWrapper"/>
        /// <para>包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>实例方法、静态方法</description>
        /// </item>
        /// </list>
        /// <para>不包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>私有方法</description>
        /// </item>
        /// </list>
        /// </summary>
        public IEnumerable<MethodWrapper> PublicMethods { get; set; }

        /// <summary>
        /// 自定义特性集合
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}