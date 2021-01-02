using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 设置参数为 Body 地址
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public sealed class BodyAttribute : RequestParameterAttribute
    {
    }
}