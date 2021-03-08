using Furion.DependencyInjection;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// System.Text.Json 序列化提供器（默认实现）
    /// </summary>
    [Injection(Order = -999)]
    public class SystemTextJsonSerializerProvider : IJsonSerializerProvider, ISingleton
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public string Serialize(object value, object jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(value, jsonSerializerOptions as JsonSerializerOptions);
        }

        /// <summary>
        /// 序列化流
        /// </summary>
        /// <param name="utf8Json"></param>
        /// <param name="value"></param>
        /// <param name="inputType"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SerializeAsync(Stream utf8Json, object value, Type inputType, object jsonSerializerOptions = null, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.SerializeAsync(utf8Json, value, inputType, jsonSerializerOptions as JsonSerializerOptions, cancellationToken);
        }

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json, object jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions as JsonSerializerOptions);
        }

        /// <summary>
        /// 反序列化流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="utf8Json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask<T> DeserializeAsync<T>(Stream utf8Json, object jsonSerializerOptions = null, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync<T>(utf8Json, jsonSerializerOptions as JsonSerializerOptions, cancellationToken);
        }
    }
}