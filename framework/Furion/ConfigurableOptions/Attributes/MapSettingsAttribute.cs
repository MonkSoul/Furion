// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.ConfigurableOptions;

/// <summary>
/// 重新映射属性配置
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MapSettingsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">appsetting.json 对应键</param>
    public MapSettingsAttribute(string path)
    {
        Path = path;
    }

    /// <summary>
    /// 对应配置文件中的路径
    /// </summary>
    public string Path { get; set; }
}