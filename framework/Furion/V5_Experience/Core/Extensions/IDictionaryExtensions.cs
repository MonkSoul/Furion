// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Extensions;

/// <summary>
///     <see cref="IDictionary{TKey, TValue}" /> 拓展类
/// </summary>
internal static class IDictionaryExtensions
{
    /// <summary>
    ///     添加或更新
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="key">
    ///     <typeparamref name="TKey" />
    /// </param>
    /// <param name="value">
    ///     <typeparamref name="TValue" />
    /// </param>
    internal static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary
        , TKey key
        , TValue value)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(value);

        // 检查键是否存在
        if (!dictionary.TryGetValue(key, out var values))
        {
            values = [];
            dictionary.Add(key, values);
        }

        values.Add(value);
    }

    /// <summary>
    ///     添加或更新
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="concatDictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    internal static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary,
        IDictionary<TKey, List<TValue>> concatDictionary)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(concatDictionary);

        // 逐条遍历合并更新
        foreach (var (key, newValues) in concatDictionary)
        {
            // 检查键是否存在
            if (!dictionary.TryGetValue(key, out var values))
            {
                values = [];
                dictionary.Add(key, values);
            }

            values.AddRange(newValues);
        }
    }

    /// <summary>
    ///     添加或更新
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="concatDictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    internal static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary,
        IDictionary<TKey, TValue> concatDictionary)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(concatDictionary);

        // 逐条遍历合并更新
        foreach (var (key, newValue) in concatDictionary)
        {
            // 检查键是否存在
            if (!dictionary.TryGetValue(key, out var values))
            {
                values = [];
                dictionary.Add(key, values);
            }

            values.Add(newValue);
        }
    }

    /// <summary>
    ///     添加或更新
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="concatDictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    internal static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        IDictionary<TKey, TValue> concatDictionary)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(concatDictionary);

        // 逐条遍历合并更新
        foreach (var (key, value) in concatDictionary)
        {
            // 检查键是否存在
            dictionary[key] = value;
        }
    }

    /// <summary>
    ///     尝试添加
    /// </summary>
    /// <remarks>其中键是由值通过给定的选择器函数生成的。</remarks>
    /// <param name="dictionary">
    ///     <see cref="IDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="values">
    ///     <see cref="IEnumerable{T}" />
    /// </param>
    /// <param name="keySelector">键选择器</param>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    internal static void TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        IEnumerable<TValue>? values, Func<TValue, TKey> keySelector)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(keySelector);

        // 空检查
        if (values is null)
        {
            return;
        }

        // 逐条遍历尝试添加
        foreach (var value in values)
        {
            dictionary.TryAdd(keySelector(value), value);
        }
    }
}