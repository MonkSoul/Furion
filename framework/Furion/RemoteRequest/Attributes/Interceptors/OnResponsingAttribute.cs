using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 用于拦截 HttpResponseMessage
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class OnResponsingAttribute : Attribute
    {
    }
}