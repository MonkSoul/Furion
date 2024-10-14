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