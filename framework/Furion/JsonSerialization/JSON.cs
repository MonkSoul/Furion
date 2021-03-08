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
    }
}