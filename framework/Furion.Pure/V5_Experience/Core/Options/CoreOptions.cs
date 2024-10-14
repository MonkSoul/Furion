// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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