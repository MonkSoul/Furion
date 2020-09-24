// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				   Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Mapster;

namespace Fur.ObjectMapper
{
    /// <summary>
    /// Mapster 对象映射
    /// </summary>
    public static class MapsterExtensions
    {
        /// <summary>
        /// 将下划线大写命名转为骆驼命名法
        /// </summary>
        /// <typeparam name="TSetter">类型适配设置</typeparam>
        /// <param name="setter"></param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns>TSetter</returns>
        public static TSetter CamelCaseNamed<TSetter>(this TSetter setter, bool ignoreCase = true)
            where TSetter : TypeAdapterSetter
        {
            setter.Settings.NameMatchingStrategy = new NameMatchingStrategy
            {
                SourceMemberNameConverter = (string name) =>
                {
                    var _name = Helpers.ToCamelCaseNamed(name);
                    return ignoreCase ? _name.ToLower() : _name;
                },
                DestinationMemberNameConverter = (string name) => ignoreCase ? name.ToLower() : name
            };
            return setter;
        }

        /// <summary>
        /// 将骆驼命名法转为骆驼命名方式
        /// </summary>
        /// <typeparam name="TSetter">类型适配设置</typeparam>
        /// <param name="setter"></param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns>TSetter</returns>
        public static TSetter UnderlineNamed<TSetter>(this TSetter setter, bool ignoreCase = true)
            where TSetter : TypeAdapterSetter
        {
            setter.Settings.NameMatchingStrategy = new NameMatchingStrategy
            {
                SourceMemberNameConverter = (string name) =>
                {
                    var _name = Helpers.ToUnderlineNamed(name);
                    return ignoreCase ? _name.ToLower() : _name;
                },
                DestinationMemberNameConverter = (string name) => ignoreCase ? name.ToLower() : name,
            };
            return setter;
        }
    }
}