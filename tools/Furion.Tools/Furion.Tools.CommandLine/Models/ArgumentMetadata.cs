// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Reflection;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数元数据
/// </summary>
public sealed class ArgumentMetadata
{
    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; internal set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; internal set; }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; internal set; }

    /// <summary>
    /// 参数值
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 是否传参
    /// </summary>
    public bool IsTransmission { get; set; }

    /// <summary>
    /// 是否集合
    /// </summary>
    public bool IsCollection { get; internal set; }

    /// <summary>
    /// 属性对象
    /// </summary>
    public PropertyInfo Property { get; internal set; }

    /// <summary>
    /// 是否传入短参数
    /// </summary>
    public bool IsShortName { get; set; }

    /// <summary>
    /// 是否传入长参数
    /// </summary>
    public bool IsLongName { get; set; }
}
