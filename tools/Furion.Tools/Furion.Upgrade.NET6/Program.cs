// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2. 
// You may obtain a copy of Mulan PSL v2 at:
//         https://gitee.com/dotnetchina/Furion/blob/master/LICENSEPSL2 
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.  
// See the Mulan PSL v2 for more details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Furion.Tools.CommandLine;
using Cmd = CliWrap;

namespace Furion.Upgrade.NET6;

class Program
{
    const string net6Version = "6.0.0";
    const string targetFrameworkFinal = "<TargetFramework>net6.0</TargetFramework>";
    const string targetFrameworkRegex = "<TargetFramework>(?<version>.+)</TargetFramework>";

    const string furionPackageRegex = @"<PackageReference\s+Include=""(?<package>Furion.*)""\s+Version=""(?<version>.+)""\s+/>";
    const string furionPackageFinal = @"<PackageReference Include=""{0}"" Version=""{1}"" />";
    const string furionVersion = "3.0.0";

    const string programeCode = @"
var builder = WebApplication.CreateBuilder(args).Inject();
var app = builder.Build();
app.Run();
";

    // 最小注入
    static void Main(string[] args) => Cli.Inject();

    [Argument('s', "source", "项目根目录")]
    static string Start { get; set; }
    static void StartHandler(ArgumentMetadata argument)
    {
        // 输出简介
        OutpuDescription();

        // 获取路径
        var sourcePath = string.IsNullOrWhiteSpace(argument.Value?.ToString()) ? Environment.CurrentDirectory : argument.Value.ToString();

        // 第一步，检测 SDK
        Step1();

        // 第二步，扫描所有 .csproj 文件
        var (files, fileNames) = Step2(sourcePath);

        // 第三步，选择启动层
        var index = Step3(fileNames);

        // 第四步，编译所有代码
        BuildProject(sourcePath);

        // 第五步，升级项目版本及 Furion 所有包版本
        Step5(files);

        // 第六步，重新编译代码
        BuildProject(sourcePath, 6);

        // 第七步，替换启动层的 Program.cs 代码
        Step7(files, index);

        // 第八步，移除启动层 Startup.cs
        Step8(files, index);

        // 第九步，编译所有代码
        BuildProject(sourcePath, 9);

        // 第十步，成功啦
        Step10();
    }

    private static void Step10()
    {
        Cli.WriteLine("======================================================", ConsoleColor.Gray);
        Cli.EmptyLine();
        Cli.Warn("恭喜你！升级成功啦！！！");
        Cli.EmptyLine();
        Cli.Warn(@"
  _    _                           _         _____                              __       _ _       
 | |  | |                         | |       / ____|                            / _|     | | |      
 | |  | |_ __   __ _ _ __ __ _  __| | ___  | (___  _   _  ___ ___ ___  ___ ___| |_ _   _| | |_   _ 
 | |  | | '_ \ / _` | '__/ _` |/ _` |/ _ \  \___ \| | | |/ __/ __/ _ \/ __/ __|  _| | | | | | | | |
 | |__| | |_) | (_| | | | (_| | (_| |  __/  ____) | |_| | (_| (_|  __/\__ \__ \ | | |_| | | | |_| |
  \____/| .__/ \__, |_|  \__,_|\__,_|\___| |_____/ \__,_|\___\___\___||___/___/_|  \__,_|_|_|\__, |
        | |     __/ |                                                                         __/ |
        |_|    |___/                                                                         |___/ 
");
    }

    /// <summary>
    /// 第八步，移除启动层 Startup.cs
    /// </summary>
    /// <param name="files"></param>
    /// <param name="index"></param>
    private static void Step8(string[] files, int index)
    {
        var startupfile = Path.Combine(Path.GetDirectoryName(files[index - 1]), "Startup.cs");
        Cli.Warn($"8. 正在移除启动层 {startupfile} 文件...");
        Cli.EmptyLine();
        if (File.Exists(startupfile))
        {
            Cli.Success(" 移除成功");
        }
        else Cli.Success($" 未找到 {startupfile}，已忽略");
        Cli.EmptyLine();
        Load();
    }

    /// <summary>
    /// 第七步，替换启动层的 Program.cs 代码
    /// </summary>
    /// <param name="files"></param>
    /// <param name="index"></param>
    private static void Step7(string[] files, int index)
    {
        var programePath = Path.Combine(Path.GetDirectoryName(files[index - 1]), "Program.cs");
        Cli.Warn($"7. 正在替换启动层 {programePath} 代码...");
        Cli.EmptyLine();

        if (File.Exists(programePath))
        {
            var oldContext = File.ReadAllText(programePath);
            Cli.WriteLine("======================================================", ConsoleColor.Gray);
            Cli.WriteLine(oldContext);
            Cli.EmptyLine();
            Cli.WriteLine("=>", ConsoleColor.Blue);
            Cli.WriteLine("=>", ConsoleColor.Blue);
            Cli.WriteLine("=>", ConsoleColor.Blue);
            Cli.WriteLine("=>", ConsoleColor.Blue);
            Cli.EmptyLine();
            Cli.Success(programeCode);
            Cli.WriteLine("======================================================", ConsoleColor.Gray);
            Cli.EmptyLine();
        }
        Load();
    }

    // 第五步，升级项目版本及 Furion 所有包版本
    private static void Step5(string[] files)
    {
        Cli.Warn("5. 正在升级项目框架版本及 Furion 版本...");
        Cli.EmptyLine();

        var targetRegex = new Regex(targetFrameworkRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        var furionRegex = new Regex(furionPackageRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        foreach (var file in files)
        {
            var content = File.ReadAllText(file);

            Cli.Tip($" 正在替换 {Path.GetFileName(file)}...");

            // 替换 <TargetFramework>(?<version>.+)</TargetFramework>
            if (targetRegex.IsMatch(content))
            {
                Cli.WriteLine("======================================================", ConsoleColor.Gray);
                Cli.Write($" {targetRegex.Match(content).Value} => ");
                content = targetRegex.Replace(content, targetFrameworkFinal);
                Cli.Write(targetFrameworkFinal + "\n", ConsoleColor.Green);
                Cli.WriteLine("======================================================", ConsoleColor.Gray);
            }

            // 升级 Furion 包
            if (furionRegex.IsMatch(content))
            {
                Cli.WriteLine("======================================================", ConsoleColor.Gray);
                foreach (Match item in furionRegex.Matches(content))
                {
                    var package = item.Groups["package"].Value;
                    var version = item.Groups["version"].Value;
                    var final = string.Format(furionPackageFinal, package, furionVersion);

                    content = content.Replace(item.Value, final);


                    Cli.Write($" {item.Value} => ");
                    Cli.Write(final + "\n", ConsoleColor.Green);
                }
                Cli.WriteLine("======================================================", ConsoleColor.Gray);
            }

            Cli.EmptyLine();
        }

        Cli.EmptyLine();
        Load();
    }

    /// <summary>
    /// 第四步，编译所有代码
    /// </summary>
    private static void BuildProject(string sourcePath, int step = 4)
    {
        Cli.Warn($"{step}. 编译生成项目及还原所有 Nuget 包...");
        Cli.EmptyLine();

        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        var result = Cmd.Cli.Wrap("dotnet")
                      .WithArguments($"build {sourcePath}")
                      .WithStandardOutputPipe(Cmd.PipeTarget.ToStringBuilder(stdOutBuffer))
                       .WithStandardErrorPipe(Cmd.PipeTarget.ToStringBuilder(stdErrBuffer))
                      .ExecuteAsync().GetAwaiter().GetResult();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();

        // 如果编译失败
        if (!string.IsNullOrWhiteSpace(stdErr) || string.IsNullOrWhiteSpace(stdOut))
        {
            Cli.Error("编译失败：");
            Cli.Error(stdErr);
            Cli.Exit();
        }

        // 输出所有编译日志
        Cli.Success(stdOut);

        Cli.EmptyLine();
        Load();
    }

    /// <summary>
    /// 第三步，选择启动层
    /// </summary>
    /// <param name="fileNames"></param>
    private static int Step3(string[] fileNames)
    {
        // 第三步，选择您启动目录
        Cli.Warn("3. 请选择您项目的启动项目（输入序号即可）...");
        Cli.EmptyLine();

        var index = Cli.ReadOptions("", fileNames);
        Cli.EmptyLine();
        Cli.Success($" 已设置启动层为：{fileNames[index - 1].Replace(".csproj", "")}");

        Cli.EmptyLine();
        Load();

        return index;
    }

    /// <summary>
    /// 第二步，扫描所有 .csproj 文件
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <returns></returns>
    private static (string[], string[]) Step2(string sourcePath)
    {
        // 第二步，扫描所有 .csproj 文件
        Cli.Warn("2. 扫描指定目录所有项目文件...");
        Cli.EmptyLine();

        var files = Directory.GetFiles(sourcePath, "*.csproj", SearchOption.AllDirectories);
        var fileNames = files.Select(u => Path.GetFileName(u)).ToArray();
        if (files.Length == 0)
        {
            Cli.Error($"这不是一个有效的解决方案目录 [{sourcePath}]");
            Cli.Exit();
        }

        // 输出所有项目文件
        foreach (var item in fileNames)
        {
            Cli.Success(" " + item);
        }

        Cli.EmptyLine();
        Load();
        return (files, fileNames);
    }

    /// <summary>
    /// 第一步，检测 SDK
    /// </summary>
    private static void Step1()
    {
        Cli.Warn("1. 检查是否安装 .NET6 最新版 SDK...");
        Cli.EmptyLine();

        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        var result = Cmd.Cli.Wrap("dotnet")
                      .WithArguments("--list-sdks")
                      .WithStandardOutputPipe(Cmd.PipeTarget.ToStringBuilder(stdOutBuffer))
                       .WithStandardErrorPipe(Cmd.PipeTarget.ToStringBuilder(stdErrBuffer))
                      .ExecuteAsync().GetAwaiter().GetResult();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();

        // 判断是否安装 SDK
        if (!string.IsNullOrWhiteSpace(stdErr) || string.IsNullOrWhiteSpace(stdOut))
        {
            Cli.Error("未检测到安装任何 .NET SDK，程序终止！复制链接下载：https://dotnet.microsoft.com/download/dotnet/6.0");
            Cli.Exit();
        }

        // 判断是否安装了最新的 .NET 6
        var sdks = stdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (!sdks.Any(u => u.StartsWith(net6Version)))
        {
            Cli.Error($"未检测到安装 .NET 6 最新 SDK（{net6Version}），程序终止！复制链接下载：https://dotnet.microsoft.com/download/dotnet/6.0");
            Cli.Exit();
        }

        // 输出所有安装的 SDK
        foreach (var item in sdks)
        {
            if (item.StartsWith(net6Version)) Cli.Success(" " + item);
            else Cli.WriteLine(" " + item);
        }

        Cli.EmptyLine();
        Load();
    }

    /// <summary>
    /// 查看版本
    /// </summary>
    [Argument('v', "version", "工具版本号")]
    static bool Version { get; set; }
    static void VersionHandler(ArgumentMetadata argument)
    {
        Console.WriteLine(Cli.GetVersion());
    }

    /// <summary>
    /// 查看帮助文档
    /// </summary>
    [Argument('h', "help", "查看帮助文档")]
    static bool Help { get; set; }
    static void HelpHandler(ArgumentMetadata argument)
    {
        Cli.GetHelpText("furion-upgrade");
    }

    /// <summary>
    /// 未匹配参数，操作符处理
    /// </summary>
    /// <param name="isEmpty"></param>
    /// <param name="operands"></param>
    /// <param name="noMatches"></param>
    static void NoMatchesHandler(bool isEmpty, string[] operands, Dictionary<string, object> noMatches)
    {
        if (isEmpty)
        {
            OutpuDescription();
        }
    }

    /// <summary>
    /// 输出简介
    /// </summary>
    static void OutpuDescription()
    {
        Console.WriteLine(@"
  ______          _               _    _                           _      
 |  ____|        (_)             | |  | |                         | |     
 | |__ _   _ _ __ _  ___  _ __   | |  | |_ __   __ _ _ __ __ _  __| | ___ 
 |  __| | | | '__| |/ _ \| '_ \  | |  | | '_ \ / _` | '__/ _` |/ _` |/ _ \
 | |  | |_| | |  | | (_) | | | | | |__| | |_) | (_| | | | (_| | (_| |  __/
 |_|   \__,_|_|  |_|\___/|_| |_|  \____/| .__/ \__, |_|  \__,_|\__,_|\___|
                                        | |     __/ |                     
                                        |_|    |___/                      
");
        Cli.WriteLine($"欢迎使用{Cli.GetDescription()}");
        Cli.WriteLine("作者：百小僧");
        Cli.WriteLine("版本：" + Cli.GetVersion());
        Cli.EmptyLine();
    }

    /// <summary>
    /// 模拟处理中
    /// </summary>
    static void Load()
    {
        Cli.Write(" 正在准备下一个操作", ConsoleColor.Blue);
        for (int i = 0; i < 6; i++)
        {
            Task.Delay(400).GetAwaiter().GetResult();
            Cli.Write(".", ConsoleColor.Blue);
        }
        Cli.EmptyLine();
        Cli.EmptyLine();
    }
}
