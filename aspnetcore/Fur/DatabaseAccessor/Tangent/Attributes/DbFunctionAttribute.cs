using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes
{
    /// <summary>
    /// 切面数据库函数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonInflated]
    public class DbFunctionAttribute : TangentCompileTypeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名称</param>
        public DbFunctionAttribute(string name) : base(name)
        {
        }
    }
}