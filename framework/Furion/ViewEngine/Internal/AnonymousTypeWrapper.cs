// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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