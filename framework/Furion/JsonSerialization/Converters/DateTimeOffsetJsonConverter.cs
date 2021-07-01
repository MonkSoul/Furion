// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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
    [SuppressSniffer]
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