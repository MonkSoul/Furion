// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Collections.Concurrent;

namespace Furion.Extensions;

/// <summary>
///     <see cref="ConcurrentDictionary{TKey, TValue}" /> 拓展类
/// </summary>
internal static class ConcurrentDictionaryExtensions
{
    /// <summary>
    ///     根据字典键更新对应的值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dictionary">
    ///     <see cref="ConcurrentDictionary{TKey, TValue}" />
    /// </param>
    /// <param name="key">
    ///     <typeparamref name="TKey" />
    /// </param>
    /// <param name="updateFactory">自定义更新委托</param>
    /// <param name="value">
    ///     <typeparamref name="TValue" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool TryUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary
        , TKey key
        , Func<TValue, TValue> updateFactory
        , out TValue? value)
        where TKey : notnull
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(updateFactory);

        // 查找字典值
        if (!dictionary.TryGetValue(key, out var oldValue))
        {
            value = default;
            return false;
        }

        // 调用自定义更新委托
        var updatedValue = updateFactory(oldValue);

        // 更新字典值
        var result = dictionary.TryUpdate(key, updatedValue, oldValue);
        value = result ? updatedValue : oldValue;

        return result;
    }
}