// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Furion.Tools.CommandLine;

/// <summary>
/// Cli 输出消息
/// </summary>
public static partial class Cli
{
    /// <summary>
    /// 输出空行
    /// </summary>
    public static void EmptyLine()
    {
        Console.WriteLine();
    }

    /// <summary>
    /// 输出一行
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="fillLine"></param>
    public static void WriteLine(object message, ConsoleColor color = ConsoleColor.White, ConsoleColor? backgroundColor = null, bool fillLine = false)
    {
        Output(message, text => Console.WriteLine(text), color, backgroundColor, fillLine);
    }

    /// <summary>
    /// 输出单个
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="fillLine"></param>
    public static void Write(object message, ConsoleColor color = ConsoleColor.White, ConsoleColor? backgroundColor = null, bool fillLine = false)
    {
        Output(message, text => Console.Write(text), color, backgroundColor, fillLine);
    }

    /// <summary>
    /// 命令输出警告
    /// </summary>
    /// <param name="message"></param>
    public static void Warn(object message)
    {
        WriteLine(message, ConsoleColor.Yellow);
    }

    /// <summary>
    /// 命令输出成功提示一行
    /// </summary>
    /// <param name="message"></param>
    public static void Success(object message)
    {
        WriteLine(message, ConsoleColor.Green);
    }

    /// <summary>
    /// 命令输出错误
    /// </summary>
    /// <param name="message"></param>
    public static void Error(object message)
    {
        WriteLine(message, ConsoleColor.Red);
    }

    /// <summary>
    /// 命令输出提示
    /// </summary>
    /// <param name="message"></param>
    public static void Tip(object message)
    {
        WriteLine(message, ConsoleColor.Blue);
    }

    /// <summary>
    /// 收集用户多行输入
    /// </summary>
    /// <returns></returns>
    public static string[] ReadInput(string tip = "Enter 'exit' to exit.")
    {
        var list = new List<string>();

        // 输出一行提示
        Warn(tip);

        // 循环读取用户输入
        while (true)
        {
            Console.Write("> ");

            // 接收用户输入
            var input = Console.ReadLine();

            // 如果用户输入 exit 则退出
            if (input == "exit") break;

            list.Add(input);
        }

        return list.ToArray();
    }

    /// <summary>
    /// 读取用户选项
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static int ReadOptions(string text, string[] options, string tip = "Enter 'exit' to exit.")
    {
        if (options == null || options.Length == 0) throw new ArgumentNullException(nameof(options));

        // 输出问题
        Repeat: Success(text);

        // 输出选项
        for (var i = 0; i < options.Length; i++)
        {
            Tip($" {i + 1}.{options[i]}");
        }

        // 输出一行提示
        Warn(tip);

        Console.Write("> ");

        // 接收用户输入
        var input = Console.ReadLine();

        // 如果用户输入 exit 则退出
        if (input == "exit") return -1;

        // 解析用户选择的选项
        var isNumber = int.TryParse(input, out var number);
        if (isNumber && number > 0 && number <= options.Length) return number;
        else goto Repeat;
    }

    /// <summary>
    /// 获取帮助
    /// </summary>
    /// <param name="keyword"></param>
    public static void GetHelpText(string keyword = default)
    {
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            Warn("USAGE:");
            Console.WriteLine($"    {keyword} [OPTIONS]");
            Console.WriteLine();
        }

        Warn("OPTIONS:");

        foreach (var item in ArgumentMetadatas)
        {
            Console.WriteLine($"    -{item.ShortName}, --{(item.LongName + " <" + item.Property.PropertyType.Name.ToLower() + ">"),-30} {item.HelpText}");
        }
    }

    /// <summary>
    /// 命令行输出文本
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="handler"></param>
    /// <param name="color">字体颜色</param>
    /// <param name="backgroundColor">背景颜色</param>
    /// <param name="fillLine">背景颜色填充一整行</param>
    private static void Output(object message, Action<string> handler, ConsoleColor color = ConsoleColor.White, ConsoleColor? backgroundColor = null, bool fillLine = false)
    {
        Console.ResetColor();
        Console.ForegroundColor = color;
        if (backgroundColor != null) Console.BackgroundColor = backgroundColor.Value;

        var text = string.Empty;
        if (message != null)
        {
            var messageType = message.GetType();
            // 如果是基元类型，直接转换字符串输出
            if (messageType.IsPrimitive) text = message.ToString();
            // 如果是数组类型，直接转换字符串输出
            else if (messageType.IsArray) text = string.Join(",", ((IList)message).Cast<string>());
            // 如果是枚举类型
            else if (messageType.IsEnum) text = Enum.GetName(messageType, message);
            else text = message.ToString();
        }

        handler(!fillLine ? text : text.PadRight(Console.WindowWidth - text.Length));
        Console.ResetColor();
    }
}
