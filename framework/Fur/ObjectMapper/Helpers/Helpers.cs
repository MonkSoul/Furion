// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur 
// 				    Github：https://github.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System.Text;
using System.Threading;

namespace Fur.ObjectMapper
{
    /// <summary>
    /// 对象映射帮助类
    /// </summary>
    [SkipScan]
    internal class Helpers
    {
        /// <summary>
        /// 将下划线大写命名转换为骆驼命名
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        internal static string ToCamelCaseNamed(string str)
        {
            if (!str.Contains('_') || !str.ToUpper().Equals(str) || str.StartsWith('_') || str.EndsWith('_')) return str;

            var stringBuilder = new StringBuilder();
            var words = str.Split('_');
            foreach (var word in words)
            {
                stringBuilder.Append(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将骆驼命名转成下划线大写命名
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        internal static string ToUnderlineNamed(string str)
        {
            var stringBuilder = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsUpper(c) && !str.StartsWith(c)) stringBuilder.Append('_');
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString().ToUpper();
        }
    }
}