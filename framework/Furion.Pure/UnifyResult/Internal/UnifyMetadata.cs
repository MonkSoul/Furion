// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.UnifyResult;

/// <summary>
/// 规范化元数据
/// </summary>
internal sealed class UnifyMetadata
{
    /// <summary>
    /// 提供器名称
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// 提供器类型
    /// </summary>
    public Type ProviderType { get; set; }

    /// <summary>
    /// 统一的结果类型
    /// </summary>
    public Type ResultType { get; set; }
}