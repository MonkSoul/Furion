﻿// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Furion.ConfigurableOptions;

/// <summary>
/// 应用选项依赖接口
/// </summary>
public partial interface IConfigurableOptions
{ }

/// <summary>
/// 选项后期配置
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public partial interface IConfigurableOptions<TOptions> : IConfigurableOptions
    where TOptions : class, IConfigurableOptions
{
    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    void PostConfigure(TOptions options, IConfiguration configuration);
}

/// <summary>
/// 带验证的应用选项依赖接口
/// </summary>
/// <typeparam name="TOptions"></typeparam>
/// <typeparam name="TOptionsValidation"></typeparam>
public partial interface IConfigurableOptions<TOptions, TOptionsValidation> : IConfigurableOptions<TOptions>
    where TOptions : class, IConfigurableOptions
    where TOptionsValidation : class, IValidateOptions<TOptions>
{
}

/// <summary>
/// 带监听的应用选项依赖接口
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public partial interface IConfigurableOptionsListener<TOptions> : IConfigurableOptions<TOptions>
    where TOptions : class, IConfigurableOptions
{
    /// <summary>
    /// 监听
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    void OnListener(TOptions options, IConfiguration configuration);
}