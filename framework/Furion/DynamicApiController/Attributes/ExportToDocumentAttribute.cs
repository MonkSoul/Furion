using Furion.DependencyInjection;
using System;

namespace Furion.DynamicApiController
{
    /// <summary>
    /// 配置 Mvc 控制器也能导出到 Swagger 中
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public sealed class ExportToDocumentAttribute : Attribute
    {
    }
}