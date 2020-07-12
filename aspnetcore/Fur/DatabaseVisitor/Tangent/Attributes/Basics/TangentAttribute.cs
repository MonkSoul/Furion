using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes.Basics
{
    /// <summary>
    /// 切面基类特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentAttribute : Attribute
    {
        /// <summary>
        /// 数据库上下文标识类
        /// </summary>
        public Type DbContextIdentifier { get; set; }

        /// <summary>
        /// 数据库执行返回值原类型
        /// </summary>
        public Type DbExecuteType { get; set; }
    }
}