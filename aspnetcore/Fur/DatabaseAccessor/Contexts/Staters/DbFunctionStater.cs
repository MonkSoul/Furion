using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Attributes;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 数据库函数状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    internal class DbFunctionStater
    {
        /// <summary>
        /// <see cref="DbEFFunctionAttribute"/> 特性对象
        /// </summary>
        internal DbEFFunctionAttribute DbEFFunctionAttribute { get; set; }
    }
}
