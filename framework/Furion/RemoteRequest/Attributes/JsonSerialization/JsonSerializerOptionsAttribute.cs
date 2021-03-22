using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 配置序列化
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class JsonSerializerOptionsAttribute : Attribute
    {
    }
}