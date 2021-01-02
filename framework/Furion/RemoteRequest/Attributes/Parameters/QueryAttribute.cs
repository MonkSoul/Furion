using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 设置参数为 Url 地址
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public sealed class QueryAttribute : RequestParameterAttribute
    {
    }
}