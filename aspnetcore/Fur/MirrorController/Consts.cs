using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Fur.MirrorController
{
    /// <summary>
    /// 模块常量配置
    /// </summary>
    internal static class Consts
    {
        /// <summary>
        /// 分组名分割器
        /// </summary>
        internal const string GroupNameSeparator = "|||";

        /// <summary>
        /// 请求行为字典
        /// </summary>
        internal static Dictionary<string, string> HttpVerbSetter { get; private set; }

        /// <summary>
        /// 数据绑定忽略类型
        /// </summary>
        internal static IEnumerable<Type> BindFromBodyIgnoreTypes { get; private set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Consts()
        {
            HttpVerbSetter = new Dictionary<string, string>()
            {
                ["post"] = "POST",
                ["add"] = "POST",
                ["create"] = "POST",
                ["insert"] = "POST",
                ["append"] = "POST",
                ["save"] = "POST",
                ["submit"] = "POST",

                ["get"] = "GET",
                ["find"] = "GET",
                ["fetch"] = "GET",
                ["query"] = "GET",
                ["search"] = "GET",

                ["put"] = "PUT",
                ["update"] = "PUT",
                ["modify"] = "PUT",
                ["change"] = "PUT",

                ["delete"] = "DELETE",
                ["remove"] = "DELETE",
                ["clear"] = "DELETE"
            };

            BindFromBodyIgnoreTypes = new List<Type>()
            {
                typeof(IFormFile)
            };
        }
    }
}