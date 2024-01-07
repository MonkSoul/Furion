// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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