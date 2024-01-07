// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 规范化序列化配置
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class UnifySerializerSettingAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    public UnifySerializerSettingAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 序列化名称
    /// </summary>
    public string Name { get; set; }
}