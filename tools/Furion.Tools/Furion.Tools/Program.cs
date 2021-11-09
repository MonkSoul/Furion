// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.Tools.CommandLine;

namespace Furion.Tools;

internal class Program
{
    // 初始化并自动填充命令参数
    static void Main(string[] args) => Cli.Inject();

    /// <summary>
    /// 版本号
    /// </summary>
    [Argument('v', "version", "版本号")]
    static string Version { get; set; }

    static void VersionHandler(ArgumentMetadata argument) => Cli.Success(Cli.GetVersion());

    /// <summary>
    /// 查看帮助
    /// </summary>
    [Argument('h', "help", "使用帮助")]
    static bool Help { get; set; }

    static void HelpHandler(ArgumentMetadata argument) => Cli.GetHelpText("furion");

    /// <summary>
    /// 选择框架
    /// </summary>
    [Argument('f', "framework", "选择喜欢的框架")]
    static bool Framework { get; set; }

    static void FrameworkHandler(ArgumentMetadata argument) => Cli.WriteLine(Cli.ReadOptions("请选择您最喜欢的框架：", new[] { "Furion", "Abp Next", "ASP.NET Core" }));

    [Argument('n', "name", "告诉我你的名字"), MaxLength(3, ErrorMessage = "名字不能超过 3 个字符")]
    static string Name { get; set; }
    static void NameHandler(ArgumentMetadata argument) => Cli.WriteLine($"Hello, {Name}");

    /// <summary>
    /// 操作符
    /// </summary>
    [Operands]
    static string[] Operands { get; set; }

    static void NoMatchesHandler(bool isEmpty, string[] operands, Dictionary<string, object> noMatches)
    {
        if (isEmpty) Cli.WriteLine($"欢迎使用 {Cli.GetDescription()}");
        if (operands.Length > 0) Cli.Error($"未找到该操作符：{string.Join(",", operands)}");
        if (noMatches.Count > 0) Cli.Error($"未找到该参数：{string.Join(",", noMatches.Keys)}");
    }
}
