// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.InstantMessaging;

/// <summary>
/// 即时通信集线器配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public sealed class MapHubAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pattern"></param>
    public MapHubAttribute(string pattern)
    {
        Pattern = pattern;
    }

    /// <summary>
    /// 配置终点路由地址
    /// </summary>
    public string Pattern { get; set; }
}