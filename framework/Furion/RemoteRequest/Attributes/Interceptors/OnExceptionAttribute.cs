using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 用于拦截请求出错
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class OnExceptionAttribute : Attribute
    {
    }
}