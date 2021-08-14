using System.Reflection;

namespace Furion.Tools.CommandLine
{
    /// <summary>
    /// 参数元数据
    /// </summary>
    public sealed class ArgumentMetadata
    {
        /// <summary>
        /// 短参数名
        /// </summary>
        public char ShortName { get; internal set; }

        /// <summary>
        /// 长参数名
        /// </summary>
        public string LongName { get; internal set; }
        /// <summary>
        /// 帮助文本
        /// </summary>
        public string HelpText { get; internal set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 是否传参
        /// </summary>
        public bool IsTransmission { get; set; }

        /// <summary>
        /// 是否集合
        /// </summary>
        public bool IsCollection { get; internal set; }

        /// <summary>
        /// 属性对象
        /// </summary>
        public PropertyInfo Property { get; internal set; }
    }
}