// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;

namespace Fur.ConfigurableOptions
{
    /// <summary>
    /// 选项配置特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public sealed class OptionsSettingsAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OptionsSettingsAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonKey">appsetting.json 对应键</param>
        public OptionsSettingsAttribute(string jsonKey)
        {
            JsonKey = jsonKey;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
        public OptionsSettingsAttribute(bool postConfigureAll)
        {
            PostConfigureAll = postConfigureAll;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonKey">appsetting.json 对应键</param>
        /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
        public OptionsSettingsAttribute(string jsonKey, bool postConfigureAll)
        {
            JsonKey = jsonKey;
            PostConfigureAll = postConfigureAll;
        }

        /// <summary>
        /// 对应配置文件中的Key
        /// </summary>
        public string JsonKey { get; set; }

        /// <summary>
        /// 对所有配置实例进行后期配置
        /// </summary>
        public bool PostConfigureAll { get; set; }
    }
}