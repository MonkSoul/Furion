using Furion.DependencyInjection;
using Furion.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// DateTimeOffset 类型序列化
    /// </summary>
    [SkipScan]
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DateTimeOffsetJsonConverter()
        {
            Format ??= "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format"></param>
        public DateTimeOffsetJsonConverter(string format)
        {
            Format = format;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format"></param>
        /// <param name="outputToLocalDateTime"></param>
        public DateTimeOffsetJsonConverter(string format, bool outputToLocalDateTime)
        {
            Format = format;
            OutputToLocalDateTime = outputToLocalDateTime;
        }

        /// <summary>
        /// 时间格式化格式
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 是否输出为为当地时间
        /// </summary>
        public bool OutputToLocalDateTime { get; set; } = false;

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            // 判断是否序列化成当地时间
            var formatDateTime = OutputToLocalDateTime ? value.ConvertToDateTime() : value;
            writer.WriteStringValue(formatDateTime.ToString(Format));
        }
    }
}