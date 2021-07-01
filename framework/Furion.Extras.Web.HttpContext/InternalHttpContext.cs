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

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// HttpContext 上下文实用类
    /// <para>代码参考自：https://www.cnblogs.com/artech/p/how-to-get-httpcontext.html </para>
    /// </summary>
    public static class InternalHttpContext
    {
        private static Func<object> _asyncLocalAccessor;
        private static Func<object, object> _holderAccessor;
        private static Func<object, HttpContext> _httpContextAccessor;

        /// <summary>
        /// 获取当前 HttpContext 对象
        /// </summary>
        /// <returns></returns>
        public static HttpContext Current()
        {
            var asyncLocal = (_asyncLocalAccessor ??= CreateAsyncLocalAccessor())();
            if (asyncLocal == null) return null;

            var holder = (_holderAccessor ??= CreateHolderAccessor(asyncLocal))(asyncLocal);
            if (holder == null) return null;

            return (_httpContextAccessor ??= CreateHttpContextAccessor(holder))(holder);

            // 创建异步本地访问器
            static Func<object> CreateAsyncLocalAccessor()
            {
                var fieldInfo = typeof(HttpContextAccessor).GetField("_httpContextCurrent", BindingFlags.Static | BindingFlags.NonPublic);
                var field = Expression.Field(null, fieldInfo);
                return Expression.Lambda<Func<object>>(field).Compile();
            }

            // 创建常驻 HttpContext 访问器
            static Func<object, object> CreateHolderAccessor(object asyncLocal)
            {
                var holderType = asyncLocal.GetType().GetGenericArguments()[0];
                var method = typeof(AsyncLocal<>).MakeGenericType(holderType).GetProperty("Value").GetGetMethod();
                var target = Expression.Parameter(typeof(object));
                var convert = Expression.Convert(target, asyncLocal.GetType());
                var getValue = Expression.Call(convert, method);
                return Expression.Lambda<Func<object, object>>(getValue, target).Compile();
            }

            // 获取 HttpContext 访问器
            static Func<object, HttpContext> CreateHttpContextAccessor(object holder)
            {
                var target = Expression.Parameter(typeof(object));
                var convert = Expression.Convert(target, holder.GetType());
                var field = Expression.Field(convert, "Context");
                var convertAsResult = Expression.Convert(field, typeof(HttpContext));
                return Expression.Lambda<Func<object, HttpContext>>(convertAsResult, target).Compile();
            }
        }
    }
}