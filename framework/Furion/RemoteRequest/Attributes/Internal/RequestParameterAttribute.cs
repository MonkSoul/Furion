using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 请求参数特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequestParameterAttribute : Attribute
    {
    }
}