// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DataValidation;
using Furion.FriendlyException;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Furion;

/// <summary>
/// AddInject 配置选项
/// </summary>
public sealed class AddInjectOptions
{
    /// <summary>
    /// 配置 Swagger Gen
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureSwaggerGen(Action<SwaggerGenOptions> configure)
    {
        SwaggerGenConfigure = configure;
    }

    /// <summary>
    /// 配置 DataValidation
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureDataValidation(Action<DataValidationOptions> configure)
    {
        DataValidationConfigure = configure;
    }

    /// <summary>
    /// 配置 FriendlyException
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureFriendlyException(Action<FriendlyExceptionOptions> configure)
    {
        FriendlyExceptionConfigure = configure;
    }

    /// <summary>
    /// Swagger Gen 配置
    /// </summary>
    internal static Action<SwaggerGenOptions> SwaggerGenConfigure { get; private set; }

    /// <summary>
    /// DataValidation 配置
    /// </summary>
    internal static Action<DataValidationOptions> DataValidationConfigure { get; private set; }

    /// <summary>
    /// FriendlyException 配置
    /// </summary>
    internal static Action<FriendlyExceptionOptions> FriendlyExceptionConfigure { get; private set; }
}