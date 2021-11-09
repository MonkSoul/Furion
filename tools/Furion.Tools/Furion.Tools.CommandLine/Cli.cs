// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Furion.Tools.CommandLine;

/// <summary>
/// Cli 常用方法
/// </summary>
public static partial class Cli
{
    /// <summary>
    /// 参数元数据集合
    /// </summary>
    public static IEnumerable<ArgumentMetadata> ArgumentMetadatas { get; internal set; }

    /// <summary>
    /// 最小侵入初始化
    /// </summary>
    public static void Inject()
    {
        foreach (var (propertyName, handler) in ArgumentHandlers)
        {
            CheckMatch(propertyName, handler);
        }

        // 没有匹配的 Handler
        var noMatchHandler = GetEntryType().DeclaredMethods.FirstOrDefault(u => u.IsStatic
                                                                   && u.Name == "NoMatchesHandler"
                                                                   && u.ReturnType == typeof(void)
                                                                   && u.GetParameters().Length == 3
                                                                   && u.GetParameters()[0].ParameterType == typeof(bool)
                                                                   && u.GetParameters()[1].ParameterType == typeof(string[])
                                                                   && u.GetParameters()[2].ParameterType == typeof(Dictionary<string, object>));

        if (noMatchHandler != null)
        {
            CheckNoMatches((Action<bool, string[], Dictionary<string, object>>)Delegate.CreateDelegate(typeof(Action<bool, string[], Dictionary<string, object>>), noMatchHandler));
        }
    }

    /// <summary>
    /// 判断参数是否定义
    /// </summary>
    /// <remarks>可通过?.IsTransmission == true 判断是否定义</remarks>
    /// <param name="argumentName"></param>
    /// <param name="handler"></param>
    public static void Check(string argumentName, Action<ArgumentMetadata> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        var argumentMetadata = ArgumentMetadatas.FirstOrDefault(u => u.ShortName.ToString() == argumentName || u.LongName == argumentName || u.Property.Name == argumentName);
        handler(argumentMetadata);
    }

    /// <summary>
    /// 检查一组命令参数
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="handler"></param>
    public static void Check(string[] arguments, Action<bool, object> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        var matchArgument = arguments.FirstOrDefault(u => Arguments.ArgumentDictionary.ContainsKey(u));

        var isMatch = matchArgument != null;
        var value = isMatch ? Arguments.ArgumentDictionary[matchArgument] : null;

        handler(isMatch, value);
    }

    /// <summary>
    /// 检查单个匹配
    /// </summary>
    /// <param name="argumentName"></param>
    /// <param name="handler"></param>
    public static void CheckMatch(string argumentName, Action<ArgumentMetadata> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        Check(argumentName, argument =>
        {
            if (argument?.IsTransmission == true) handler(argument);
        });
    }

    /// <summary>
    /// 检查一组参数匹配
    /// </summary>
    /// <param name="argumentName"></param>
    /// <param name="handler"></param>
    public static void CheckMatch(string[] arguments, Action<object> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        Check(arguments, (isMatch, value) =>
        {
            if (isMatch) handler(value);
        });
    }

    /// <summary>
    /// 检查所有未匹配参数、操作符
    /// </summary>
    /// <param name="handler">arg1: 是否传递空参数，arg2：操作符列表，args3：未匹配的参数列表</param>
    /// <returns></returns>
    public static void CheckNoMatches(Action<bool, string[], Dictionary<string, object>> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        // 所有匹配的字符
        var matches = ArgumentMetadatas.Select(u => u.ShortName.ToString())
                                                        .Concat(ArgumentMetadatas.Select(u => u.LongName));

        // 未匹配字符
        var noMatches = Arguments.ArgumentDictionary.Where(u => !matches.Contains(u.Key)).ToDictionary(u => u.Key, u => u.Value);

        // 操作符
        var operands = Arguments.OperandList.Where(u => !u.Contains(Assembly.GetEntryAssembly().GetName().Name)).ToArray();

        handler(Arguments.ArgumentDictionary.Count == 0 && operands.Length == 0, operands, noMatches);
    }

    /// <summary>
    /// 获取当前工具版本
    /// </summary>
    /// <returns></returns>
    public static string GetVersion()
    {
        return Assembly.GetEntryAssembly()
                            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                            .InformationalVersion
                            .ToString();
    }

    /// <summary>
    /// 获取当前工具描述
    /// </summary>
    /// <returns></returns>
    public static string GetDescription()
    {
        return Assembly.GetEntryAssembly()
                           .GetCustomAttribute<AssemblyDescriptionAttribute>()
                           .Description
                           .ToString();
    }

    /// <summary>
    /// 解析命令行参数
    /// </summary>
    /// <param name="commandLineString"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ArgumentModel Parse()
    {
        return MapperTo(Arguments.Parse(u =>
         {
             u.TargetType = GetEntryType();
         }));
    }

    /// <summary>
    /// 解析命令行参数
    /// </summary>
    /// <param name="commandLineString"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ArgumentModel Parse(string commandLineString, Action<ArgumentParseOptions> configure = null)
    {
        return MapperTo(Arguments.Parse(commandLineString, configure));
    }

    /// <summary>
    /// 解析命令行参数
    /// </summary>
    /// <param name="commandLineString"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ArgumentModel Parse(string commandLineString, ArgumentParseOptions options)
    {
        return MapperTo(Arguments.Parse(commandLineString, options));
    }

    /// <summary>
    /// 强制退出控制台（终止执行）
    /// </summary>
    public static void Exit()
    {
        Environment.Exit(Environment.ExitCode);
    }

    /// <summary>
    /// 映射
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    private static ArgumentModel MapperTo(Arguments arguments)
    {
        return new ArgumentModel
        {
            ArgumentDictionary = arguments.ArgumentDictionary,
            ArgumentList = arguments.ArgumentList,
            CommandLineString = arguments.CommandLineString,
            OperandList = arguments.OperandList
        };
    }
}
