// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 配置 ADO.NET 超时时间
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class TimeoutAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="seconds"></param>
    public TimeoutAttribute(int seconds)
    {
        Seconds = seconds;
    }

    /// <summary>
    /// 超时时间（秒）
    /// </summary>
    public int Seconds { get; set; }
}