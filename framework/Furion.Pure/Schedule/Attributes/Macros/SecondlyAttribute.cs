// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 每秒开始作业触发器特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class SecondlyAttribute : CronAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SecondlyAttribute()
        : base("@secondly")
    {
    }
}