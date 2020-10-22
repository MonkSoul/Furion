using Fur.DependencyInjection;
using System;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// 设置请问报文头
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class HeadersAttribute : Attribute
    {
    }
}