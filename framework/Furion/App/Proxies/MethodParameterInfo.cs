using System.Reflection;

namespace Furion
{
    /// <summary>
    /// 方法参数信息
    /// </summary>
    internal class MethodParameterInfo
    {
        /// <summary>
        /// 参数
        /// </summary>
        internal ParameterInfo Parameter { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        internal object Value { get; set; }
    }
}