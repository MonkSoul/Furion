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