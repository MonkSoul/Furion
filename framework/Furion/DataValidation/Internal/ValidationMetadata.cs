// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Furion.DataValidation;

/// <summary>
/// 验证信息元数据
/// </summary>
public sealed class ValidationMetadata
{
    /// <summary>
    /// 验证结果
    /// </summary>
    /// <remarks>返回字典或字符串类型</remarks>
    public object ValidationResult { get; internal set; }

    /// <summary>
    /// 异常消息
    /// </summary>
    public string Message { get; internal set; }

    /// <summary>
    /// 验证状态
    /// </summary>
    public ModelStateDictionary ModelState { get; internal set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public object ErrorCode { get; internal set; }

    /// <summary>
    /// 错误码（没被复写过的 ErrorCode ）
    /// </summary>
    public object OriginErrorCode { get; internal set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public int? StatusCode { get; internal set; }

    /// <summary>
    /// 首个错误属性
    /// </summary>
    public string FirstErrorProperty { get; internal set; }

    /// <summary>
    /// 首个错误消息
    /// </summary>
    public string FirstErrorMessage { get; internal set; }

    /// <summary>
    /// 额外数据
    /// </summary>
    public object Data { get; internal set; }
}