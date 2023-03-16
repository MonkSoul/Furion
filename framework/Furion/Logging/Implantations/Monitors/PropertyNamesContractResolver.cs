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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Furion.Logging;

/// <summary>
/// 支持忽略特定属性的 CamelCase 序列化
/// </summary>
internal sealed class CamelCasePropertyNamesContractResolverWithIgnoreProperties : CamelCasePropertyNamesContractResolver
{
    /// <summary>
    /// 被忽略的属性名称
    /// </summary>
    private readonly string[] _names;

    /// <summary>
    /// 被忽略的属性类型
    /// </summary>
    private readonly Type[] _type;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="names"></param>
    /// <param name="types"></param>
    public CamelCasePropertyNamesContractResolverWithIgnoreProperties(string[] names, Type[] types)
    {
        _names = names ?? Array.Empty<string>();
        _type = types ?? Array.Empty<Type>();
    }

    /// <summary>
    /// 重写需要序列化的属性名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="memberSerialization"></param>
    /// <returns></returns>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var allProperties = base.CreateProperties(type, memberSerialization);

        return allProperties.Where(p =>
                !_names.Contains(p.PropertyName, StringComparer.OrdinalIgnoreCase)
                && !_type.Contains(p.PropertyType)).ToList();
    }
}

/// <summary>
/// 支持忽略特定属性的 Default 序列化
/// </summary>
internal sealed class DefaultContractResolverWithIgnoreProperties : DefaultContractResolver
{
    /// <summary>
    /// 被忽略的属性名称
    /// </summary>
    private readonly string[] _names;

    /// <summary>
    /// 被忽略的属性类型
    /// </summary>
    private readonly Type[] _type;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="names"></param>
    /// <param name="types"></param>
    public DefaultContractResolverWithIgnoreProperties(string[] names, Type[] types)
    {
        _names = names ?? Array.Empty<string>();
        _type = types ?? Array.Empty<Type>();
    }

    /// <summary>
    /// 重写需要序列化的属性名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="memberSerialization"></param>
    /// <returns></returns>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var allProperties = base.CreateProperties(type, memberSerialization);

        return allProperties.Where(p =>
                !_names.Contains(p.PropertyName, StringComparer.OrdinalIgnoreCase)
                && !_type.Contains(p.PropertyType)).ToList();
    }
}