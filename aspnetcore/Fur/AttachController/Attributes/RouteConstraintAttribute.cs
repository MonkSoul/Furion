using System;

namespace Fur.AttachController.Attributes
{
    /// <summary>
    /// 设置路由约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RouteConstraintAttribute : Attribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="constraint"></param>
        public RouteConstraintAttribute(string constraint) => Constraint = constraint;
        /// <summary>
        /// 约束，支持?，int，:int?，:min(10)
        /// </summary>
        public string Constraint { get; set; }
    }
}
