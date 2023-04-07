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

namespace Furion.ViewEngine;

/// <summary>
/// 匿名类型包装器
/// </summary>
[SuppressSniffer]
public class AnonymousTypeWrapper : DynamicObject
{
    /// <summary>
    /// 匿名模型
    /// </summary>
    private readonly object model;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="model"></param>
    public AnonymousTypeWrapper(object model)
    {
        this.model = model;
    }

    /// <summary>
    /// 获取成员信息
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        var propertyInfo = model.GetType().GetProperty(binder.Name);

        if (propertyInfo == null)
        {
            result = null;
            return false;
        }

        result = propertyInfo.GetValue(model, null);

        if (result == null)
        {
            return true;
        }

        var type = result.GetType();

        if (result.IsAnonymous())
        {
            result = new AnonymousTypeWrapper(result);
        }

        var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
        if (isEnumerable && result is not string)
        {
            var actType = type.IsArray ? type.GetElementType() : type.GenericTypeArguments[0];

            // https://gitee.com/dotnetchina/Furion/pulls/773
            // 修复集合的泛型类型为匿名类型时类型转换
            var genericType = actType.IsAnonymous()
                ? typeof(List<AnonymousTypeWrapper>)
                : typeof(List<>).MakeGenericType(actType);

            // 创建集合实例
            var list = Activator.CreateInstance(genericType);
            var addMethod = list.GetType().GetMethod("Add");

            var data = result as IEnumerable;
            foreach (var item in data)
            {
                addMethod.Invoke(list, new object[] {
                    item.IsAnonymous() ? new AnonymousTypeWrapper(item) : item
                });
            }

            result = list;
        }

        return true;
    }
}