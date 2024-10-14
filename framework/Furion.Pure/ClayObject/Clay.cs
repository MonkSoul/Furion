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

using Furion.ClayObject.Extensions;
using Furion.Extensions;
using Furion.JsonSerialization;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Furion.ClayObject;

/// <summary>
/// 粘土对象
/// </summary>
/// <remarks>实现动态对象，类似 JavaScript 对象操作</remarks>
[SuppressSniffer]
public sealed class Clay : DynamicObject, IEnumerable
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    public Clay(bool throwOnUndefined = true)
    {
        XmlElement = new XElement("root", CreateTypeAttr(JsonType.@object));
        jsonType = JsonType.@object;
        ThrowOnUndefined = throwOnUndefined;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="element"><see cref="XElement"/></param>
    /// <param name="type">JSON 类型</param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    private Clay(XElement element, JsonType type, bool throwOnUndefined = true)
    {
        // 只允许 object 和 array 类型
        Debug.Assert(type == JsonType.array || type == JsonType.@object);

        XmlElement = element;
        jsonType = type;
        ThrowOnUndefined = throwOnUndefined;
    }

    /// <summary>
    /// JSON 类型
    /// </summary>
    private enum JsonType
    {
        @string, number, boolean, @object, array, @null
    }

    /// <summary>
    /// 是否是 Object 类型
    /// </summary>
    public bool IsObject => jsonType == JsonType.@object;

    /// <summary>
    /// 是否是 Array 类型
    /// </summary>
    public bool IsArray => jsonType == JsonType.array;

    /// <summary>
    /// 粘土对象 Xml 元数据
    /// </summary>
    public XElement XmlElement { get; private set; }

    /// <summary>
    /// 当 Clay 时 数组类型时的长度
    /// </summary>
    public int Length => XmlElement.Elements().Count();

    /// <summary>
    /// 配置读取不存在 Key 时行为
    /// </summary>
    /// <remarks>如果设置 false，那么返回 null</remarks>
    public bool ThrowOnUndefined { get; set; } = true;

    /// <summary>
    /// 创建空的粘土对象
    /// </summary>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Object(bool throwOnUndefined = true)
    {
        return new Clay(throwOnUndefined);
    }

    /// <summary>
    /// 基于现有对象创建粘土对象
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Object(object obj, bool throwOnUndefined = true)
    {
        // 空检查
        if (obj != null)
        {
            var json = CreateJsonString(new XStreamingElement("root", CreateTypeAttr(GetJsonType(obj)), CreateJsonNode(obj)));
            return Parse(json, throwOnUndefined);
        }

        throw new ArgumentNullException(nameof(obj));
    }

    /// <summary>
    /// 基于现有对象创建粘土对象
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Parse(string json, bool throwOnUndefined = true)
    {
        return Parse(json, Encoding.UTF8, throwOnUndefined);
    }

    /// <summary>
    /// 基于现有对象创建粘土对象
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <param name="encoding">编码类型</param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Parse(string json, Encoding encoding, bool throwOnUndefined = true)
    {
        using var reader = JsonReaderWriterFactory.CreateJsonReader(encoding.GetBytes(json), XmlDictionaryReaderQuotas.Max);
        return ToValue(XElement.Load(reader), throwOnUndefined);
    }

    /// <summary>
    /// 基于 Stream 对象创建粘土对象
    /// </summary>
    /// <param name="stream"><see cref="Stream"/></param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Parse(Stream stream, bool throwOnUndefined = true)
    {
        using var reader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max);
        return ToValue(XElement.Load(reader), throwOnUndefined);
    }

    /// <summary>
    /// 基于 Stream 对象创建粘土对象
    /// </summary>
    /// <param name="stream"><see cref="Stream"/></param>
    /// <param name="encoding">编码类型</param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns><see cref="Clay"/></returns>
    public static dynamic Parse(Stream stream, Encoding encoding, bool throwOnUndefined = true)
    {
        using var reader = JsonReaderWriterFactory.CreateJsonReader(stream, encoding, XmlDictionaryReaderQuotas.Max, _ => { });
        return ToValue(XElement.Load(reader), throwOnUndefined);
    }

    /// <summary>
    /// 重写动态调用方法实现删除行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="args"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
        result = (IsArray)
            ? Delete((int)args[0])
            : Delete((string)args[0]);
        return true;
    }

    /// <summary>
    /// 重写动态调用成员名称方法实现键是否存在行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="args"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
        if (args.Length > 0)
        {
            result = null;
            return false;
        }

        result = IsDefined(binder.Name);

        return true;
    }

    /// <summary>
    /// 重写类型转换方法实现粘土对象动态转换
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryConvert(ConvertBinder binder, out object result)
    {
        if (binder.Type == typeof(IEnumerable) || binder.Type == typeof(object[]))
        {
            var ie = (IsArray)
                ? XmlElement.Elements().Select(x => ToValue(x, ThrowOnUndefined))
                : XmlElement.Elements().Select(x => (dynamic)new KeyValuePair<string, object>(x.Name == "{item}item" ? x.Attribute("item").Value : x.Name.LocalName, ToValue(x, ThrowOnUndefined)));
            result = (binder.Type == typeof(object[])) ? ie.ToArray() : ie;
        }
        else
        {
            result = Deserialize(binder.Type);
        }

        return true;
    }

    /// <summary>
    /// 重写根据索引获取值的行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="indexes"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
    {
        return (IsArray)
            ? TryGet(XmlElement.Elements().ElementAtOrDefault((int)indexes[0]), out result, ThrowOnUndefined)
            : TryGet(FindXElement((string)indexes[0], out _), out result, ThrowOnUndefined);
    }

    /// <summary>
    /// 重写根据成员名称获取值的行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return (IsArray)
            ? TryGet(XmlElement.Elements().ElementAtOrDefault(int.Parse(binder.Name)), out result, ThrowOnUndefined)
            : TryGet(FindXElement(binder.Name, out _), out result, ThrowOnUndefined);
    }

    /// <summary>
    /// 重写根据索引设置值的行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="indexes"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
    {
        return (IsArray)
            ? TrySet((int)indexes[0], value)
            : TrySet((string)indexes[0], value);
    }

    /// <summary>
    /// 重写根据成员名称设置值的行为
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        return (IsArray)
            ? TrySet(int.Parse(binder.Name), value)
            : TrySet(binder.Name, value);
    }

    /// <summary>
    /// 重写获取所有动态成员名称行为
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<string> GetDynamicMemberNames()
    {
        return (IsArray)
            ? XmlElement.Elements().Select((x, i) => i.ToString())
            : XmlElement.Elements().Select(x => x.Name == "{item}item" ? x.Attribute("item").Value : x.Name.LocalName);
    }

    /// <summary>
    /// 重写转换成字符串方法
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        // 清除 null 节点
        ClearNullNodes();

        return CreateJsonString(new XStreamingElement("root", CreateTypeAttr(jsonType), XmlElement.Elements()));
    }

    /// <summary>
    /// 清除 null 节点
    /// </summary>
    private void ClearNullNodes()
    {
        foreach (var elem in XmlElement.Descendants().Where(x => x.Attribute("type").Value == "null"))
        {
            elem.RemoveNodes();
        }
    }

    /// <summary>
    /// 判断对象键是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsDefined(string name)
    {
        return IsObject && (FindXElement(name, out _) != null);
    }

    /// <summary>
    /// 判断数组索引是否存在
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool IsDefined(int index)
    {
        return IsArray && (XmlElement.Elements().ElementAtOrDefault(index) != null);
    }

    /// <summary>
    /// 根据键删除对象属性
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Delete(string name)
    {
        var elem = FindXElement(name, out _);
        if (elem != null)
        {
            elem.Remove();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 根据索引删除数组元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool Delete(int index)
    {
        var elem = XmlElement.Elements().ElementAtOrDefault(index);
        if (elem != null)
        {
            elem.Remove();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 将粘土对象反序列化为特定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Deserialize<T>()
    {
        return (T)Deserialize(typeof(T));
    }

    /// <summary>
    /// 将粘土对象转换为 object 类型
    /// </summary>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public object Solidify(object jsonSerializerOptions = null)
    {
        return Solidify<object>(jsonSerializerOptions);
    }

    /// <summary>
    /// 将粘土对象转换为特定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public T Solidify<T>(object jsonSerializerOptions = null)
    {
        return JSON.Deserialize<T>(ToString(), jsonSerializerOptions);
    }

    /// <summary>
    /// 将粘土对象转换为字典类型
    /// </summary>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public IDictionary<string, object> ToDictionary(object jsonSerializerOptions = null)
    {
        // 数组类型不支持转换成字典
        if (IsArray) throw new InvalidOperationException("Cannot convert a clay object with JsonType as an array to a dictionary object.");

        return Solidify<Dictionary<string, object>>(jsonSerializerOptions);
    }

    /// <summary>
    /// 转换成特定对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="valueProvider">值提供器</param>
    /// <returns></returns>
    public IEnumerable<T> ConvertTo<T>(Func<PropertyInfo, object, object> valueProvider = null)
        where T : class, new()
    {
        // 获取所有公开实例属性
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite);

        dynamic clay = this;
        var list = new List<T>();

        // 处理对象类型
        if (IsObject)
        {
            list.Add(ConvertTo<T>(properties, clay, valueProvider));
            return list;
        }

        // 处理数组类型
        if (IsArray)
        {
            var dynamicList = AsEnumerator<dynamic>();
            foreach (var clayItem in dynamicList)
            {
                list.Add(ConvertTo<T>(properties, clayItem, valueProvider));
            }
        }

        return list;
    }

    /// <summary>
    /// 将粘土对象转换为特定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="properties"></param>
    /// <param name="clay"></param>
    /// <param name="valueProvider">值提供器</param>
    /// <returns></returns>
    private static T ConvertTo<T>(IEnumerable<PropertyInfo> properties, dynamic clay, Func<PropertyInfo, object, object> valueProvider = null)
        where T : class, new()
    {
        var instance = Activator.CreateInstance<T>();
        foreach (var property in properties)
        {
            object value = clay.IsDefined(property.Name) ? clay[property.Name] : default;
            if (valueProvider != null)
            {
                value = valueProvider(property, value);
            }

            property.SetValue(instance, value.ChangeType(property.PropertyType));
        }

        return instance;
    }

    /// <summary>
    /// XElement 对象转换成 C# 对象
    /// </summary>
    /// <param name="element"></param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns></returns>
    private static dynamic ToValue(XElement element, bool throwOnUndefined = true)
    {
        var type = (JsonType)Enum.Parse(typeof(JsonType), element.Attribute("type").Value);
        return type switch
        {
            JsonType.boolean => (bool)element,
            JsonType.number when element.Value.Contains('.') => (double)element,
            JsonType.number => (long)element,
            JsonType.@string => (string)element,
            JsonType.@object or JsonType.array => new Clay(element, type, throwOnUndefined),
            _ => null,
        };
    }

    /// <summary>
    /// 获取 JSON 类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static JsonType GetJsonType(object obj)
    {
        if (obj == null) return JsonType.@null;

        if (obj is Clay clay)
        {
            return clay.jsonType;
        }

        var objType = obj.GetType();

        // 将特别类型转换成 string
        if (ToBeConvertStringTypes.Contains(objType)) return JsonType.@string;

        // 处理循环 Clay 类型
        if (obj is ExpandoObject) return JsonType.@object;

        return Type.GetTypeCode(objType) switch
        {
            TypeCode.Boolean => JsonType.boolean,
            TypeCode.String or TypeCode.Char or TypeCode.DateTime => JsonType.@string,
            TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Single or TypeCode.Double or TypeCode.Decimal or TypeCode.SByte or TypeCode.Byte => JsonType.number,
            TypeCode.Object => (obj is IEnumerable) ? JsonType.array : JsonType.@object,
            _ => JsonType.@null,
        };
    }

    /// <summary>
    /// 创建 XElement type 属性
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static XAttribute CreateTypeAttr(JsonType type)
    {
        return new XAttribute("type", type.ToString());
    }

    /// <summary>
    /// 创建 XElement 节点值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static object CreateJsonNode(object obj)
    {
        var isEnum = obj?.GetType().IsEnum == true;

        var type = GetJsonType(obj);
        return type switch
        {
            JsonType.@string or JsonType.number => isEnum ? (int)obj : obj,
            JsonType.boolean => obj.ToString().ToLower(),
            JsonType.@object => CreateXObject(obj),
            JsonType.array => CreateXArray(obj as IEnumerable),
            _ => null,
        };
    }

    /// <summary>
    /// 创建 XStreamingElement 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static IEnumerable<XStreamingElement> CreateXArray<T>(T obj) where T : IEnumerable
    {
        if (obj is Clay clay)
        {
            clay.ClearNullNodes();
            return clay.XmlElement.Elements().Select(xElement => new XStreamingElement(xElement.Name, xElement.Attributes(), xElement.Nodes()));
        }

        return obj.Cast<object>()
            .Select(o => new XStreamingElement("item", CreateTypeAttr(GetJsonType(o)), CreateJsonNode(o)));
    }

    /// <summary>
    /// 创建 XStreamingElement 对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static IEnumerable<XStreamingElement> CreateXObject(object obj)
    {
        if (obj is Clay clay)
        {
            clay.ClearNullNodes();
            return clay.XmlElement.Elements().Select(xElement => new XStreamingElement(xElement.Name, xElement.Attributes(), xElement.Nodes()));
        }

        if (obj is ExpandoObject expando)
        {
            var dict = (IDictionary<string, object>)expando;
            return dict.Select(a => new XStreamingElement(a.Key, CreateTypeAttr(GetJsonType(a.Value)), CreateJsonNode(a.Value)));
        }

        return obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(pi => new { pi.Name, Value = pi.GetValue(obj, null) })
            .Select(a => new XStreamingElement(a.Name, CreateTypeAttr(GetJsonType(a.Value)), CreateJsonNode(a.Value)));
    }

    /// <summary>
    /// 创建 JSON 字符串
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private static string CreateJsonString(XStreamingElement element)
    {
        using var ms = new MemoryStream();
        using var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8);
        element.WriteTo(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    /// <summary>
    /// JSON 类型
    /// </summary>
    private readonly JsonType jsonType;

    /// <summary>
    /// 读取值
    /// </summary>
    /// <param name="element"></param>
    /// <param name="result"></param>
    /// <param name="throwOnUndefined"></param>
    /// <returns></returns>
    private static bool TryGet(XElement element, out object result, bool throwOnUndefined = true)
    {
        if (element == null)
        {
            result = null;
            return !throwOnUndefined;   // 如果这里返回 true，那么不存在的 Key 不会报错
        }

        result = ToValue(element, throwOnUndefined);
        return true;
    }

    /// <summary>
    /// 根据键设置对象值
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool TrySet(string name, object value)
    {
        var type = GetJsonType(value);
        var element = FindXElement(name, out var isValid);

        var newValue = value is Clay clay
            ? clay.XmlElement.Elements()
            : CreateJsonNode(value);

        if (element == null)
        {
            if (isValid)
            {
                XmlElement.Add(new XElement(name, CreateTypeAttr(type), newValue));
            }
            else
            {
                var xmlString = $"<a:item xmlns:a=\"item\" item=\"{name}\" type=\"{type}\"></a:item>";
                var xEle = XElement.Parse(xmlString);
                xEle.ReplaceNodes(newValue);
                XmlElement.Add(xEle);
            }
        }
        else
        {
            element.Attribute("type").Value = type.ToString();
            element.ReplaceNodes(newValue);
        }

        return true;
    }

    /// <summary>
    /// 根据索引设置数组值
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool TrySet(int index, object value)
    {
        var type = GetJsonType(value);
        var element = XmlElement.Elements().ElementAtOrDefault(index);

        var newValue = value is Clay clay
            ? clay.XmlElement.Elements()
            : CreateJsonNode(value);

        if (element == null)
        {
            XmlElement.Add(new XElement("item", CreateTypeAttr(type),
                newValue));
        }
        else
        {
            element.Attribute("type").Value = type.ToString();
            element.ReplaceNodes(newValue);
        }

        return true;
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private object Deserialize(Type type)
    {
        return (IsArray) ? DeserializeArray(type) : DeserializeObject(type);
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private object DeserializeObject(Type targetType)
    {
        var result = Activator.CreateInstance(targetType);
        var dict = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite)
            .ToDictionary(pi => pi.Name, pi => pi);

        foreach (var xElement in XmlElement.Elements())
        {
            // 获取节点真实标签名
            var localName = xElement.Name == "{item}item"
                ? xElement.Attribute("item").Value
                : xElement.Name.LocalName;

            if (!dict.TryGetValue(localName, out var propertyInfo)) continue;
            var value = Clay.DeserializeValue(xElement, propertyInfo.PropertyType, ThrowOnUndefined);
            propertyInfo.SetValue(result, value, null);
        }
        return result;
    }

    /// <summary>
    /// 反序列化值
    /// </summary>
    /// <param name="element"></param>
    /// <param name="elementType"></param>
    /// <param name="throwOnUndefined">如果设置 false，则读取不存在的值返回 null，默认 true</param>
    /// <returns></returns>
    private static dynamic DeserializeValue(XElement element, Type elementType, bool throwOnUndefined = true)
    {
        var value = ToValue(element, throwOnUndefined);

        if (value is Clay clay)
        {
            value = clay.Deserialize(elementType);
        }

        return ObsoleteObjectExtensions.ChangeType(value, elementType);
    }

    /// <summary>
    /// 反序列化数组
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private object DeserializeArray(Type targetType)
    {
        if (targetType.IsArray)
        {
            var elemType = targetType.GetElementType();
            dynamic array = Array.CreateInstance(elemType, XmlElement.Elements().Count());
            var index = 0;
            foreach (var item in XmlElement.Elements())
            {
                array[index++] = Clay.DeserializeValue(item, elemType, ThrowOnUndefined);
            }
            return array;
        }
        else
        {
            var elemType = targetType.GetGenericArguments()[0];
            dynamic list = Activator.CreateInstance(targetType);
            foreach (var item in XmlElement.Elements())
            {
                list.Add(Clay.DeserializeValue(item, elemType, ThrowOnUndefined));
            }
            return list;
        }
    }

    /// <summary>
    /// 根据键查找 <see cref="XElement"/> 对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isValid"></param>
    /// <returns></returns>
    private XElement FindXElement(string name, out bool isValid)
    {
        // 校验 Name 是否是合法的
        var validName = isValid = TryVerifyNCName(name) == null;

        var xelement = XmlElement.Elements("{item}item")
            .Where(e => (string)e.Attribute("item") == name)
            .FirstOrDefault();

        return xelement ?? (validName ? XmlElement.Element(name) : null);
    }

    /// <summary>
    /// 校验 Xml 标签格式
    /// </summary>
    private static readonly Func<string, Exception> TryVerifyNCName = (Func<string, Exception>)Delegate.CreateDelegate(typeof(Func<string, Exception>), typeof(XmlConvert).GetMethod("TryVerifyNCName", BindingFlags.Static | BindingFlags.NonPublic));

    /// <summary>
    /// 初始化粘土对象枚举器
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
        return IsArray
            ? new ClayArrayEnumerator(this)
            : new ClayObjectEnumerator(this);
    }

    /// <summary>
    /// 将粘土对象转换成 IEnumerable{T} 对象
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> AsEnumerator<T>()
    {
        return ((IEnumerable)this).Cast<T>();
    }

    /// <summary>
    /// 转换为枚举对象
    /// </summary>
    /// <returns></returns>
    public IEnumerable<dynamic> AsEnumerable()
    {
        return AsEnumerator<dynamic>();
    }

    /// <summary>
    /// 内部粘土对象枚举器
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// 将被转换成字符串的类型
    /// </summary>
    private static readonly Type[] ToBeConvertStringTypes = new[]
    {
        typeof(DateTimeOffset)
    };
}