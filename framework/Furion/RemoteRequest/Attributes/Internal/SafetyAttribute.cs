using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 安全请求，出错不抛异常
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class SafetyAttribute : Attribute
    {
    }
}