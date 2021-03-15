using Furion.DependencyInjection;
using Furion.JsonSerialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace System.Text.Json
{
    /// <summary>
    /// System.Text.Json 拓展
    /// </summary>
    [SkipScan]
    public static class SystemTextJsonExtensions
    {
        /// <summary>
        /// 添加时间格式化
        /// </summary>
        /// <param name="converters"></param>
        /// <param name="formatString"></param>
        public static void AddDateFormatString(this IList<JsonConverter> converters, string formatString)
        {
            converters.Add(new DateTimeJsonConverter(formatString));
            converters.Add(new DateTimeOffsetJsonConverter(formatString));
        }
    }
}