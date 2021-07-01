// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Furion.ViewEngine
{
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

            if (isEnumerable && !(result is string))
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
}