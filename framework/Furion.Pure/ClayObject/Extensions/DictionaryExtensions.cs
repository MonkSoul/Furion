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