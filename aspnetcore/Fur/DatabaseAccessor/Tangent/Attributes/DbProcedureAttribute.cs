using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes
{
    /// <summary>
    /// 切面存储过程特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class DbProcedureAttribute : TangentCompileTypeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">存储过程名</param>
        public DbProcedureAttribute(string name) : base(name)
        {
        }

        /// <summary>
        /// 是否带输入值或返回值（回馈值）
        /// </summary>
        public bool HasFeedback { get; set; } = false;
    }
}