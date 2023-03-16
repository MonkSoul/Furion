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