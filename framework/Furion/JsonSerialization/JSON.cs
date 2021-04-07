using Furion.DependencyInjection;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// JSON 静态帮助类
    /// </summary>
    [SkipScan]
    public static class JSON
    {
        /// <summary>
        /// 获取 JSON 序列化提供器
        /// </summary>
        /// <returns></returns>
        public static IJsonSerializerProvider GetJsonSerializer()
        {
            return App.GetService<IJsonSerializerProvider>();
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static string Serialize(object value, object jsonSerializerOptions = default)
        {
            return GetJsonSerializer().Serialize(value, jsonSerializerOptions);
        }

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json, object jsonSerializerOptions = default)
        {
            return GetJsonSerializer().Deserialize<T>(json, jsonSerializerOptions);
        }
    }
}