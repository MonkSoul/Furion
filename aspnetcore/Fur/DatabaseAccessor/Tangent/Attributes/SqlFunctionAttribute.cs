using Fur.AppCore.Attributes;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 切面数据库函数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonInflated]
    public class SqlFunctionAttribute : TangentCompileTypeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名称</param>
        public SqlFunctionAttribute(string name) : base(name)
        {
        }
    }
}