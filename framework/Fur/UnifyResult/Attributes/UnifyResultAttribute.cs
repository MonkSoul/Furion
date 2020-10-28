using Fur;
using Fur.DependencyInjection;
using Fur.UnifyResult;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.AspNetCore.Mvc.Core
{
    /// <summary>
    /// 规范化结果配置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UnifyResultAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="statusCode"></param>
        public UnifyResultAttribute(int statusCode) : base(statusCode)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public UnifyResultAttribute(Type type) : base(type, StatusCodes.Status200OK)
        {
            WrapType(type);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="statusCode"></param>
        public UnifyResultAttribute(Type type, int statusCode) : base(type, statusCode)
        {
            WrapType(type);
        }

        /// <summary>
        /// 保证类型
        /// </summary>
        /// <param name="type"></param>
        private void WrapType(Type type)
        {
            if (type != null && !type.HasImplementedRawGeneric(typeof(RESTfulResult<>)))
            {
                base.Type = typeof(RESTfulResult<>).MakeGenericType(type);
            }
        }
    }
}