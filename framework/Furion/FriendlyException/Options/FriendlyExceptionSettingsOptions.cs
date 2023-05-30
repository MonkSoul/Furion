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