using System.Collections.Generic;

namespace Furion.Tools.CommandLine
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public sealed class ArgumentModel
    {
        /// <summary>
        /// 参数字典
        /// </summary>
        public Dictionary<string, object> ArgumentDictionary { get; internal set; }

        /// <summary>
        /// 参数键值对
        /// </summary>
        public List<KeyValuePair<string, string>> ArgumentList { get; internal set; }

        /// <summary>
        /// 参数命令
        /// </summary>
        public string CommandLineString { get; internal set; }

        /// <summary>
        /// 操作符列表
        /// </summary>
        public List<string> OperandList { get; internal set; }
    }
}