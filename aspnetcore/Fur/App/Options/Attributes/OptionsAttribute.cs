using System;

namespace Fur.Options
{
    /// <summary>
    /// 选项配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OptionsAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonKey">appsetting.json 对应键</param>
        public OptionsAttribute(string jsonKey)
            => JsonKey = jsonKey;

        /// <summary>
        /// 对应配置文件中的Key
        /// </summary>
        public string JsonKey { get; set; }
    }
}