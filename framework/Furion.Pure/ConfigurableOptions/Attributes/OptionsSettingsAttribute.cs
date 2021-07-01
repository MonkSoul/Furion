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
using System;

namespace Furion.ConfigurableOptions
{
    /// <summary>
    /// 选项配置特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
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