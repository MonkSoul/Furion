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

using Microsoft.Extensions.Options;

namespace Furion.Options;

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    bool Validate(TOptions options);
}

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
/// <typeparam name="TDep">依赖服务</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions, TDep> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
    where TDep : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    /// <param name="dep">依赖服务</param>
    bool Validate(TOptions options, TDep dep);
}

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
/// <typeparam name="TDep1">依赖服务</typeparam>
/// <typeparam name="TDep2">依赖服务</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions, TDep1, TDep2> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
    where TDep1 : class
    where TDep2 : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    /// <param name="dep1">依赖服务</param>
    /// <param name="dep2">依赖服务</param>
    bool Validate(TOptions options
        , TDep1 dep1
        , TDep2 dep2);
}

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
/// <typeparam name="TDep1">依赖服务</typeparam>
/// <typeparam name="TDep2">依赖服务</typeparam>
/// <typeparam name="TDep3">依赖服务</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions, TDep1, TDep2, TDep3> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
    where TDep1 : class
    where TDep2 : class
    where TDep3 : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    /// <param name="dep1">依赖服务</param>
    /// <param name="dep2">依赖服务</param>
    /// <param name="dep3">依赖服务</param>
    bool Validate(TOptions options
        , TDep1 dep1
        , TDep2 dep2
        , TDep3 dep3);
}

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
/// <typeparam name="TDep1">依赖服务</typeparam>
/// <typeparam name="TDep2">依赖服务</typeparam>
/// <typeparam name="TDep3">依赖服务</typeparam>
/// <typeparam name="TDep4">依赖服务</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions, TDep1, TDep2, TDep3, TDep4> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
    where TDep1 : class
    where TDep2 : class
    where TDep3 : class
    where TDep4 : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    /// <param name="dep1">依赖服务</param>
    /// <param name="dep2">依赖服务</param>
    /// <param name="dep3">依赖服务</param>
    /// <param name="dep4">依赖服务</param>
    bool Validate(TOptions options
        , TDep1 dep1
        , TDep2 dep2
        , TDep3 dep3
        , TDep4 dep4);
}

/// <summary>
/// 选项验证依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
/// <typeparam name="TDep1">依赖服务</typeparam>
/// <typeparam name="TDep2">依赖服务</typeparam>
/// <typeparam name="TDep3">依赖服务</typeparam>
/// <typeparam name="TDep4">依赖服务</typeparam>
/// <typeparam name="TDep5">依赖服务</typeparam>
[OptionsBuilderMethodMap(nameof(OptionsBuilder<TOptions>.Validate), false)]
public interface IValidateOptionsBuilder<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5> : IOptionsBuilderDependency<TOptions>
    where TOptions : class
    where TDep1 : class
    where TDep2 : class
    where TDep3 : class
    where TDep4 : class
    where TDep5 : class
{
    /// <summary>
    /// 复杂验证
    /// </summary>
    /// <param name="options">选项实例</param>
    /// <param name="dep1">依赖服务</param>
    /// <param name="dep2">依赖服务</param>
    /// <param name="dep3">依赖服务</param>
    /// <param name="dep4">依赖服务</param>
    /// <param name="dep5">依赖服务</param>
    bool Validate(TOptions options
        , TDep1 dep1
        , TDep2 dep2
        , TDep3 dep3
        , TDep4 dep4
        , TDep5 dep5);
}