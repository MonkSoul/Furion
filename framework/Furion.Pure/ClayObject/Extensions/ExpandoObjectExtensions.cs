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

using Furion.Extensions;
using System.Dynamic;
using System.Text.Json;

namespace Furion.ClayObject.Extensions;

/// <summary>
/// ExpandoObject 对象拓展
/// </summary>
[SuppressSniffer]
public static class ExpandoObjectExtensions
{
    /// <summary>
    /// 将对象转 ExpandoObject 类型
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ExpandoObject ToExpandoObject(this object value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (value is Clay clay && clay.IsObject)
        {
            dynamic clayExpando = new ExpandoObject();
            var dic = (IDictionary<string, object>)clayExpando;

            foreach (KeyValuePair<string, dynamic> item in (dynamic)clay)
            {
                dic.Add(item.Key, item.Value is Clay v ? v.ToExpandoObject() : item.Value);
            }

            return clayExpando;
        }

        if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
        {
            dynamic clayExpando = new ExpandoObject();
            var dic = (IDictionary<string, object>)clayExpando;
            var objDic = jsonElement.ToObject() as IDictionary<string, object>;

            foreach (var item in objDic)
            {
                dic.Add(item);
            }

            return clayExpando;
        }

        if (value is not ExpandoObject expando)
        {
            expando = new ExpandoObject();
            var dict = (IDictionary<string, object>)expando;

            var dictionary = value.ToDictionary();
            foreach (var kvp in dictionary)
            {
                dict.Add(kvp);
            }
        }

        return expando;
    }

    /// <summary>
    /// 移除 ExpandoObject 对象属性
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <param name="propertyName"></param>
    public static void RemoveProperty(this ExpandoObject expandoObject, string propertyName)
    {
        if (expandoObject == null)
            throw new ArgumentNullException(nameof(expandoObject));

        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        ((IDictionary<string, object>)expandoObject).Remove(propertyName);
    }

    /// <summary>
    /// 判断 ExpandoObject 是否为空
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static bool Empty(this ExpandoObject expandoObject)
    {
        return !((IDictionary<string, object>)expandoObject).Any();
    }

    /// <summary>
    /// 判断 ExpandoObject 是否拥有某属性
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static bool HasProperty(this ExpandoObject expandoObject, string propertyName)
    {
        if (expandoObject == null)
            throw new ArgumentNullException(nameof(expandoObject));

        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        return ((IDictionary<string, object>)expandoObject).ContainsKey(propertyName);
    }

    /// <summary>
    /// 实现 ExpandoObject 浅拷贝
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static ExpandoObject ShallowCopy(this ExpandoObject expandoObject)
    {
        return Copy(expandoObject, false);
    }

    /// <summary>
    /// 实现 ExpandoObject 深度拷贝
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static ExpandoObject DeepCopy(this ExpandoObject expandoObject)
    {
        return Copy(expandoObject, true);
    }

    /// <summary>
    /// 拷贝 ExpandoObject 对象
    /// </summary>
    /// <param name="original"></param>
    /// <param name="deep"></param>
    /// <returns></returns>
    private static ExpandoObject Copy(ExpandoObject original, bool deep)
    {
        var clone = new ExpandoObject();

        var _original = (IDictionary<string, object>)original;
        var _clone = (IDictionary<string, object>)clone;

        foreach (var kvp in _original)
        {
            _clone.Add(
                kvp.Key,
                deep && kvp.Value is ExpandoObject eObject ? DeepCopy(eObject) : kvp.Value
            );
        }

        return clone;
    }
}