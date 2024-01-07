// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.SpecificationDocument;

/// <summary>
/// 用于控制 Swager 生成 Enum 类型
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
public sealed class EnumToNumberAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public EnumToNumberAttribute()
        : this(true)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="enabled">启用状态</param>
    public EnumToNumberAttribute(bool enabled = true)
    {
        Enabled = enabled;
    }

    /// <summary>
    /// 启用状态
    /// </summary>
    /// <remarks>设置 false 则使用字符串类型</remarks>
    public bool Enabled { get; set; }
}