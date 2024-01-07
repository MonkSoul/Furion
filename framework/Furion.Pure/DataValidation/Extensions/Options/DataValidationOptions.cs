// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DataValidation;

/// <summary>
/// AddInject 数据验证配置选项
/// </summary>
public sealed class DataValidationOptions
{
    /// <summary>
    /// 启用全局数据验证
    /// </summary>
    public bool GlobalEnabled { get; set; } = true;

    /// <summary>
    /// 禁止C# 8.0 验证非可空引用类型
    /// </summary>
    public bool SuppressImplicitRequiredAttributeForNonNullableReferenceTypes { get; set; } = true;

    /// <summary>
    /// 是否禁用模型验证过滤器
    /// </summary>
    /// <remarks>只会改变启用全局验证的情况，也就是 <see cref="GlobalEnabled"/> 为 true 的情况</remarks>
    public bool SuppressModelStateInvalidFilter { get; set; } = true;

    /// <summary>
    /// 是否禁用映射异常
    /// </summary>
    public bool SuppressMapClientErrors { get; set; } = false;
}