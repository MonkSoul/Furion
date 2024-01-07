// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.UnifyResult;

/// <summary>
/// 规范化模型特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public class UnifyModelAttribute : Attribute
{
    /// <summary>
    /// 规范化模型
    /// </summary>
    /// <param name="modelType"></param>
    public UnifyModelAttribute(Type modelType)
    {
        ModelType = modelType;
    }

    /// <summary>
    /// 模型类型（泛型）
    /// </summary>
    public Type ModelType { get; set; }
}