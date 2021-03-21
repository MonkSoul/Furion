using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 代理参数基类特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterBaseAttribute : Attribute
    {
    }
}