using Fur.ApplicationBase.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationBase.Wrappers
{
    /// <summary>
    /// 方法包装类
    /// <code>sealed</code>
    /// <para>主要用来装载解决方案项目中方法常用属性及附加属性， 避免重复反射读取</para>
    /// </summary>
    [NonWrapper]
    public sealed class MethodWrapper
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        public Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 所在类型
        /// </summary>
        public Type ThisDeclareType { get; set; }

        /// <summary>
        /// 被包装的方法
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 是否是控制器Action类型。参见：<see cref="Fur.ApplicationBase.AppGlobal.IsControllerActionType(MethodInfo)"/>
        /// </summary>
        public bool IsControllerActionType { get; set; }

        /// <summary>
        /// 是否是静态方法
        /// </summary>
        public bool IsStaticMethod { get; set; }

        /// <summary>
        /// Swagger 接口文档 分组名
        /// <para>生成 Swagger 接口文档时，用于匹配特定组信息。</para>
        /// <para>参见：<see cref="Fur.MirrorController.Attributes.MirrorActionAttribute.SwaggerGroups"/></para>
        /// </summary>
        public string[] SwaggerGroups { get; set; }

        /// <summary>
        /// 方法参数包装类集合。参见：<see cref="ParameterWrapper"/>
        /// </summary>
        public IEnumerable<ParameterWrapper> Parameters { get; set; }

        /// <summary>
        /// 自定义特性集合
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}