// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
            result = ((IEnumerable<object>)result)
                    .Select(e =>
                    {
                        if (e.IsAnonymous())
                        {
                            return new AnonymousTypeWrapper(e);
                        }

                        return e;
                    })
                    .ToList();
        }

        return true;
    }
}