// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// 配置表名称前后缀
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TableFixsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    public TableFixsAttribute(string prefix)
    {
        Prefix = prefix;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    public TableFixsAttribute(string prefix, string suffix)
        : this(prefix)
    {
        Suffix = suffix;
    }

    /// <summary>
    /// 前缀
    /// </summary>
    /// <remarks>前缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Prefix { get; set; }

    /// <summary>
    /// 后缀
    /// </summary>
    /// <remarks>后缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Suffix { get; set; }
}