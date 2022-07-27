// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Options;

/// <summary>
/// 选项构建器特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OptionsBuilderAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public OptionsBuilderAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sectionKey">配置节点</param>
    public OptionsBuilderAttribute(string sectionKey)
    {
        SectionKey = sectionKey;
    }

    /// <summary>
    /// 配置节点
    /// </summary>
    public string SectionKey { get; set; }

    /// <summary>
    /// 未知配置节点抛异常
    /// </summary>
    public bool ErrorOnUnknownConfiguration { get; set; }

    /// <summary>
    /// 绑定非公开属性
    /// </summary>
    public bool BindNonPublicProperties { get; set; }

    /// <summary>
    /// 启用验证特性支持
    /// </summary>
    public bool ValidateDataAnnotations { get; set; }

    /// <summary>
    /// 验证选项类型
    /// </summary>
    public Type[] ValidateOptionsTypes { get; set; }
}