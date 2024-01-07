// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.ConfigurableOptions;

/// <summary>
/// 选项配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public sealed class OptionsSettingsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public OptionsSettingsAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">appsetting.json 对应键</param>
    public OptionsSettingsAttribute(string path)
    {
        Path = path;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
    public OptionsSettingsAttribute(bool postConfigureAll)
    {
        PostConfigureAll = postConfigureAll;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">appsetting.json 对应键</param>
    /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
    public OptionsSettingsAttribute(string path, bool postConfigureAll)
    {
        Path = path;
        PostConfigureAll = postConfigureAll;
    }

    /// <summary>
    /// 对应配置文件中的路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 对所有配置实例进行后期配置
    /// </summary>
    public bool PostConfigureAll { get; set; }
}