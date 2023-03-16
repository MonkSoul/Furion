// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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