// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace Furion.ClayObject.Extensions;

/// <summary>
/// 字典类型拓展类
/// </summary>
[SuppressSniffer]
public static class DictionaryExtensions
{
    /// <summary>
    /// 将对象转成字典
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IDictionary<string, object> ToDictionary(this object input)
    {
        if (input == null) return default;

        // 处理 JObject 类型
        if (input is JObject jobj)
        {
            var dic = new Dictionary<string, object>();
            foreach (var (key, value) in jobj)
            {
                dic.Add(key, value);
            }

            return dic;
        }

        // 处理本就是字典类型
        if (input.GetType().HasImplementedRawGeneric(typeof(IDictionary<,>)))
        {
            if (input is ExpandoObject expandObject)
            {
                return expandObject;
            }

            var dic = new Dictionary<string, object>();
            var dicInput = ((IDictionary)input);
            foreach (var key in dicInput.Keys)
            {
                dic.Add(key.ToString(), dicInput[key]);
            }

            return dic;
        }

        // 处理粘土对象
        if (input is Clay clay && clay.IsObject)
        {
            return clay.ToDictionary();
        }

        // 处理 JSON 类型
        if (input is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
        {
            return jsonElement.ToObject() as IDictionary<string, object>;
        }

        var valueType = input.GetType();

        // 判断是否是 struct 结构类型
        var isStruct = !valueType.IsPrimitive && !valueType.IsEnum && valueType.IsValueType;

        // 处理基元类型，集合类型
        if (!isStruct && (valueType.IsRichPrimitive()
            || valueType.IsArray
            || (typeof(IEnumerable).IsAssignableFrom(valueType)
                && valueType.IsGenericType)))
        {
            return new Dictionary<string, object>()
            {
                { "data",input }
            };
        }

        // 剩下的当对象处理
        var properties = input.GetType().GetProperties();
        var fields = input.GetType().GetFields();
        var members = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>());

        return members.ToDictionary(m => m.Name, m => GetValue(input, m));
    }

    /// <summary>
    /// 将对象转字典类型，其中值返回原始类型 Type 类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IDictionary<string, Tuple<Type, object>> ToDictionaryWithType(this object input)
    {
        if (input == null) return default;

        // 处理本就是字典类型
        if (input.GetType().HasImplementedRawGeneric(typeof(IDictionary<,>)))
        {
            var dicInput = ((IDictionary)input);

            var dic = new Dictionary<string, Tuple<Type, object>>();
            foreach (var key in dicInput.Keys)
            {
                var value = dicInput[key];
                var tupleValue = value == null ?
                    new Tuple<Type, object>(typeof(object), value) :
                    new Tuple<Type, object>(value.GetType(), value);

                dic.Add(key.ToString(), tupleValue);
            }

            return dic;
        }

        var dict = new Dictionary<string, Tuple<Type, object>>();

        // 获取所有属性列表
        foreach (var property in input.GetType().GetProperties())
        {
            dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));
        }

        // 获取所有成员列表
        foreach (var field in input.GetType().GetFields())
        {
            dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));
        }

        return dict;
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    private static object GetValue(object obj, MemberInfo member)
    {
        if (member is PropertyInfo info)
            return info.GetValue(obj, null);

        if (member is FieldInfo info1)
            return info1.GetValue(obj);

        throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
    }
}