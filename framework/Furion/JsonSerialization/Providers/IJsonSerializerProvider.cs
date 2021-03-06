using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// Json 序列化提供器
    /// </summary>
    public interface IJsonSerializerProvider
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        string Serialize(object value, object jsonSerializerOptions = default);

        /// <summary>
        /// 序列化流
        /// </summary>
        /// <param name="utf8Json"></param>
        /// <param name="value"></param>
        /// <param name="inputType"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SerializeAsync(Stream utf8Json, object value, Type inputType, object jsonSerializerOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        T Deserialize<T>(string json, object jsonSerializerOptions = default);

        /// <summary>
        /// 反序列化流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="utf8Json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<T> DeserializeAsync<T>(Stream utf8Json, object jsonSerializerOptions = null, CancellationToken cancellationToken = default);
    }
}