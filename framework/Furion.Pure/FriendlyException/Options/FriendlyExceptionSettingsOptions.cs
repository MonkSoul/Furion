// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Furion.FriendlyException;

/// <summary>
/// 友好异常配置选项
/// </summary>
public sealed class FriendlyExceptionSettingsOptions : IConfigurableOptions<FriendlyExceptionSettingsOptions>
{
    /// <summary>
    /// 隐藏错误码
    /// </summary>
    public bool? HideErrorCode { get; set; }

    /// <summary>
    /// 默认错误码
    /// </summary>
    public string DefaultErrorCode { get; set; }

    /// <summary>
    /// 默认错误消息
    /// </summary>
    public string DefaultErrorMessage { get; set; }

    /// <summary>
    /// 标记 Oops.Oh 为业务异常
    /// </summary>
    /// <remarks>也就是不会进入异常处理</remarks>
    public bool? ThrowBah { get; set; }

    /// <summary>
    /// 是否输出异常日志
    /// </summary>
    public bool? LogError { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(FriendlyExceptionSettingsOptions options, IConfiguration configuration)
    {
        options.HideErrorCode ??= false;
        options.DefaultErrorCode ??= string.Empty;
        //options.DefaultErrorMessage ??= "Internal Server Error";
        options.ThrowBah ??= false;
        options.LogError ??= true;
    }
}