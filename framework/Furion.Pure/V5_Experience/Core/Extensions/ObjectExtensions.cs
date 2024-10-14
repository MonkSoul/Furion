﻿// ------------------------------------------------------------------------
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

using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Furion.Extensions;

/// <summary>
///     <see cref="object" /> 拓展类
/// </summary>
internal static class ObjectExtensions
{
    /// <summary>
    ///     获取对象所在的程序集
    /// </summary>
    /// <param name="obj">
    ///     <see cref="object" />
    /// </param>
    /// <returns>
    ///     <see cref="Assembly" />
    /// </returns>
    internal static Assembly? GetAssembly(this object? obj) => obj?.GetType().Assembly;

    /// <summary>
    ///     将对象转换为基于特定文化的字符串表示形式
    /// </summary>
    /// <param name="obj">
    ///     <see cref="object" />
    /// </param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="enumAsString">指示是否将枚举类型的值作为名称输出，默认为 <c>true</c>。若为 <c>false</c>，则输出枚举的值</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? ToCultureString(this object? obj, CultureInfo culture, bool enumAsString = true)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(culture);

        return obj switch
        {
            null => null,
            string s => s,
            DateTime dt => dt.ToString("o", culture),
            DateTimeOffset df => df.ToString("o", culture),
            DateOnly od => od.ToString("yyyy-MM-dd", culture),
            TimeOnly ot => ot.ToString("HH':'mm':'ss", culture),
            Enum e when enumAsString => e.ToString(),
            Enum e => Convert.ChangeType(e, Enum.GetUnderlyingType(e.GetType())).ToString(),
            _ => obj.ToString()
        };
    }

    /// <summary>
    ///     尝试获取对象的数量
    /// </summary>
    /// <param name="obj">
    ///     <see cref="object" />
    /// </param>
    /// <param name="count">数量</param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool TryGetCount(this object obj, out int count)
    {
        // 处理可直接获取长度的类型
        switch (obj)
        {
            // 检查对象是否是字符类型
            case char:
                count = 1;
                return true;
            // 检查对象是否是字符串类型
            case string text:
                count = text.Length;
                return true;
            // 检查对象是否实现了 ICollection 接口
            case ICollection collection:
                count = collection.Count;
                return true;
        }

        // 反射查找是否存在 Count 属性
        var runtimeProperty = obj.GetType()
            .GetRuntimeProperty("Count");

        // 反射获取 Count 属性值
        if (runtimeProperty is not null
            && runtimeProperty.CanRead
            && runtimeProperty.PropertyType == typeof(int))
        {
            count = (int)runtimeProperty.GetValue(obj)!;
            return true;
        }

        count = -1;
        return false;
    }

    /// <summary>
    ///     将对象转换为 <see cref="IDictionary{TKey,TValue}" /> 类型对象
    /// </summary>
    /// <param name="obj">
    ///     <see cref="object" />
    /// </param>
    /// <returns>
    ///     <see cref="IDictionary{TKey,TValue}" />
    /// </returns>
    /// <exception cref="NotSupportedException"></exception>
    internal static IDictionary<object, object?>? ObjectToDictionary(this object? obj)
    {
        // 空检查
        if (obj is null)
        {
            return null;
        }

        // 获取对象类型
        var objType = obj.GetType();

        // 初始化不受支持的类型转换的异常消息字符串
        var notSupportedExceptionMessage =
            $"Conversion of parameter 'obj' from type `{objType}` to type `IDictionary<object, object?>` is not supported.";

        // 检查类型是否是基本类型或 void 类型
        if (objType.IsBasicType() || objType == typeof(void))
        {
            throw new NotSupportedException(notSupportedExceptionMessage);
        }

        // 检查类型是否是枚举类型
        if (objType.IsEnum)
        {
            // 转换为字典类型并返回
            return new Dictionary<object, object?> { { Enum.GetName(objType, obj)!, Convert.ToInt32(obj) } };
        }

        // 检查类型是否是 KeyValuePair<,> 单个类型
        if (objType.IsKeyValuePair())
        {
            // 获取 Key 和 Value 属性值访问器
            var getters = objType.GetKeyValuePairOrJPropertyGetters();

            // 转换为字典类型并返回
            return new Dictionary<object, object?> { { getters.KeyGetter(obj)!, getters.ValueGetter(obj) } };
        }

        // 处理 System.Text.Json 类型
        switch (obj)
        {
            case JsonDocument jsonDocument:
                return jsonDocument.RootElement.ObjectToDictionary();
            case JsonElement { ValueKind: JsonValueKind.Object } jsonElement:
                // 转换为字典类型并返回
                return jsonElement.EnumerateObject().ToDictionary<JsonProperty, object, object?>(
                    jsonProperty => jsonProperty.Name,
                    jsonProperty => jsonProperty.Value);
        }

        // 检查类型是否是键值对集合类型
        if (objType.IsKeyValueCollection(out var isKeyValuePairCollection))
        {
            // === 处理 Hashtable 和 NameValueCollection 集合类型 ===
            switch (obj)
            {
                case Hashtable hashtable:
                    return hashtable.Cast<DictionaryEntry>().ToDictionary(entry => entry.Key, entry => entry.Value);
                case NameValueCollection nameValueCollection:
                    return nameValueCollection
                        .AllKeys
                        .ToDictionary(
                            object (key) => key!, object? (key) => nameValueCollection[key]);
            }

            // === 处理非 KeyValuePair<,> 集合类型 ===
            if (!isKeyValuePairCollection)
            {
                // 将对象转化为 IDictionary 接口对象
                var dictionaryObj = (IDictionary)obj;

                // 转换为字典类型并返回
                return dictionaryObj.Count == 0
                    ? new Dictionary<object, object?>()
                    : dictionaryObj.Keys
                        .Cast<object?>()
                        .ToDictionary(key => key!, key => dictionaryObj[key!]);
            }

            // === 处理 KeyValuePair<,> 集合类型 ===
            var keyValuePairs = ((IEnumerable)obj).Cast<object?>().ToArray();

            // 空检查
            if (keyValuePairs.Length == 0)
            {
                return new Dictionary<object, object?>();
            }

            // 获取 KeyValuePair<,> 集合中元素类型
            var keyValuePairType = keyValuePairs.First()?.GetType()!;

            // 获取 Key 和 Value 属性值访问器
            var getters = keyValuePairType.GetKeyValuePairOrJPropertyGetters();

            // 转换为字典类型并返回
            return keyValuePairs.ToDictionary(keyValuePair => getters.KeyGetter(keyValuePair!)!,
                keyValuePair => getters.ValueGetter(keyValuePair!));
        }

        try
        {
            // 尝试查找对象类型的所有公开且可读的实例属性集合并转换为字典类型并返回
            return objType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.CanRead)
                .ToDictionary(object (property) => property.Name, property => property.GetValue(obj));
        }
        catch (Exception e)
        {
            throw new AggregateException(
                new NotSupportedException(notSupportedExceptionMessage), e);
        }
    }
}