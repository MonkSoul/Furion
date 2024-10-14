// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using System.Collections.Concurrent;

namespace Furion;

/// <summary>
///     核心模块选项
/// </summary>
internal sealed class CoreOptions
{
    /// <summary>
    ///     已注册的组件元数据集合
    /// </summary>
    internal readonly ConcurrentDictionary<string, ComponentMetadata> _metadataOfRegistered;

    /// <summary>
    ///     子选项集合
    /// </summary>
    internal readonly ConcurrentDictionary<Type, object> _optionsInstances;

    /// <summary>
    ///     <inheritdoc cref="CoreOptions" />
    /// </summary>
    internal CoreOptions()
    {
        _optionsInstances = new ConcurrentDictionary<Type, object>();
        _metadataOfRegistered = new ConcurrentDictionary<string, ComponentMetadata>(StringComparer.OrdinalIgnoreCase);

        EntryComponentTypes = [];
    }

    /// <summary>
    ///     入口组件类型集合
    /// </summary>
    internal HashSet<Type> EntryComponentTypes { get; init; }

    /// <summary>
    ///     获取子选项
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TOptions" />
    /// </returns>
    internal TOptions GetOrAdd<TOptions>()
        where TOptions : class, new()
    {
        // 获取子选项类型
        var optionsType = typeof(TOptions);

        // 如果不存在则添加
        _ = _optionsInstances.TryAdd(optionsType, new TOptions());

        return (TOptions)_optionsInstances[optionsType];
    }

    /// <summary>
    ///     移除子选项
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    internal void Remove<TOptions>()
        where TOptions : class, new()
    {
        // 获取子选项类型
        var optionsType = typeof(TOptions);

        // 如果存在则移除
        _ = _optionsInstances.TryRemove(optionsType, out _);
    }

    /// <summary>
    ///     登记组件注册信息
    /// </summary>
    /// <param name="metadata">
    ///     <see cref="ComponentMetadata" />
    /// </param>
    internal void TryRegisterComponent(ComponentMetadata metadata) =>
        _metadataOfRegistered.TryAdd(metadata.Name, metadata);
}