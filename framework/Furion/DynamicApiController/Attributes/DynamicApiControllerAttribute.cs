using Furion.DependencyInjection;
using System;

namespace Furion.DynamicApiController
{
    /// <summary>
    /// 动态 WebApi特性接口
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DynamicApiControllerAttribute : Attribute
    {
    }
}