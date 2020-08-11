using Microsoft.Extensions.Options;

namespace Fur.Options
{
    /// <summary>
    /// 应用选项依赖接口
    /// </summary>
    public partial interface IFurOptions { }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public partial interface IFurOptions<TOptions> : IFurOptions
        where TOptions : class, IFurOptions
    {
        void PostConfigure(TOptions options) { }
    }

    /// <summary>
    /// 带验证的应用选项依赖接口
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TOptionsValidation"></typeparam>
    public partial interface IFurOptions<TOptions, TOptionsValidation> : IFurOptions<TOptions>
        where TOptions : class, IFurOptions
        where TOptionsValidation : class, IValidateOptions<TOptions>
    {
    }
}