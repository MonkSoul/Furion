using Furion.Tools.CommandLine;
using System;

namespace Furion.Tools
{
    /// <summary>
    /// 本地安装必须找到 .nupkg 所在目录
    /// dotnet tool install -g Furion.Tools --add-source ./
    /// dotnet tool install -g Furion.Tools --version 0.0.1
    /// dotnet tool uninstall -g Furion.Tools
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 查看版本
            Cli.Check(nameof(Version), u => Cli.Success("0.0.3"));

            // 查看帮助
            Cli.Check(nameof(Help), u => Cli.GetHelpText("furion"));

            // 选择框架
            Cli.Check(nameof(Framework), u => Cli.WriteLine(Cli.ReadOptions("请选择您最喜欢的框架：", new[] { "Furion", "Abp Next", "ASP.NET Core" })));

            // 没有命令的时候输出
            Cli.CheckEmpty(() => Console.WriteLine("欢迎使用 Furion.Tools 工具。"));
        }

        /// <summary>
        /// 版本号
        /// </summary>
        [Argument('v', "version", "版本号")]
        private static string Version { get; set; }

        /// <summary>
        /// 查看帮助
        /// </summary>
        [Argument('h', "help", "使用帮助")]
        private static bool Help { get; set; }

        /// <summary>
        /// 选择框架
        /// </summary>
        [Argument('f', "framework", "选择喜欢的框架")]
        public static bool Framework { get; set; }

        /// <summary>
        /// 未匹配的操作符
        /// </summary>
        [Operands]
        private static string[] Operands { get; set; }
    }
}