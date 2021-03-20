using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 用于拦截 HttpRequestMessage
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class OnRequestAttribute : Attribute
    {
    }
}