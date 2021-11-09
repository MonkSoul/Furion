// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数定义特性
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ArgumentAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="shortName">短参数名</param>
    /// <param name="longName">长参数名</param>
    /// <param name="helpText">帮助文本</param>
    public ArgumentAttribute(char shortName, string longName, string helpText = null)
    {
        ShortName = shortName;
        LongName = longName;
        HelpText = helpText;
    }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; set; }
}
