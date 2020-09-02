// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fur.Configurable
{
    /// <summary>
    /// 应用选项依赖接口
    /// </summary>
    public partial interface IAppOptions { }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public partial interface IAppOptions<TOptions> : IAppOptions
        where TOptions : class, IAppOptions
    {
        void PostConfigure(TOptions options, IConfiguration configuration);
    }

    /// <summary>
    /// 带验证的应用选项依赖接口
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TOptionsValidation"></typeparam>
    public partial interface IAppOptions<TOptions, TOptionsValidation> : IAppOptions<TOptions>
        where TOptions : class, IAppOptions
        where TOptionsValidation : class, IValidateOptions<TOptions>
    {
    }

    /// <summary>
    ///带监听的应用选项依赖接口
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public partial interface IAppOptionsListener<TOptions>
        where TOptions : class, IAppOptions
    {
        void OnListener(TOptions options, IConfiguration configuration);
    }
}