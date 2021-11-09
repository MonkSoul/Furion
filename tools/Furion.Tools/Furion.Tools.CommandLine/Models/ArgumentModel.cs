// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Collections.Generic;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数模型
/// </summary>
public sealed class ArgumentModel
{
    /// <summary>
    /// 参数字典
    /// </summary>
    public Dictionary<string, object> ArgumentDictionary { get; internal set; }

    /// <summary>
    /// 参数键值对
    /// </summary>
    public List<KeyValuePair<string, string>> ArgumentList { get; internal set; }

    /// <summary>
    /// 参数命令
    /// </summary>
    public string CommandLineString { get; internal set; }

    /// <summary>
    /// 操作符列表
    /// </summary>
    public List<string> OperandList { get; internal set; }
}
