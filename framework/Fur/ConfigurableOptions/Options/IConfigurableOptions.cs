// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fur.ConfigurableOptions
{
    /// <summary>
    /// 应用选项依赖接口
    /// </summary>
    public partial interface IConfigurableOptions { }

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
}