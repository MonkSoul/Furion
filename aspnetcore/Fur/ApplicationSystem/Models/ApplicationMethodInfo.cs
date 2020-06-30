using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationSystem.Models
{
    /// <summary>
    /// 应用方法信息类
    /// </summary>
    public sealed class ApplicationMethodInfo
    {
        /// <summary>
        /// 所属程序集
        /// </summary>
        public Assembly Assembly { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public Type DeclareType { get; set; }
        /// <summary>
        /// 所属方法
        /// </summary>
        public MethodInfo Method { get; set; }
        /// <summary>
        /// 方法自定义特性
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
        /// <summary>
        /// 返回值类型
        /// </summary>
        public Type ReturnType { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public IEnumerable<ApplicationParameterInfo> Parameters { get; set; }
        /// <summary>
        /// 判断是否是控制器Action类型
        /// </summary>
        public bool IsControllerActionType { get; set; }
        /// <summary>
        /// Swagger 文档分组
        /// </summary>
        public string[] SwaggerGroups { get; set; }
    }
}
