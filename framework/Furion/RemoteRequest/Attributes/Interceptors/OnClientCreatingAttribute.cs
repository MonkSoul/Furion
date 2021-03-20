using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 用于拦截 HttpClient
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class OnClientCreatingAttribute : Attribute
    {
    }
}