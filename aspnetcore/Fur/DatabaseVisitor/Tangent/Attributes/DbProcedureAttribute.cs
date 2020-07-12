using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    /// <summary>
    /// 切面存储过程特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DbProcedureAttribute : TangentCompileTypeAttribute
    {
        #region 构造函数 + public DbProcedureAttribute(string name) : base(name)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">存储过程名</param>
        public DbProcedureAttribute(string name) : base(name)
        {
        }
        #endregion

        /// <summary>
        /// 是否带输入值或返回值
        /// </summary>
        public bool WithOutputOrReturn { get; set; } = false;
    }
}