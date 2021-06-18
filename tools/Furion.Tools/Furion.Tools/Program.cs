using Furion.Tools.CommandLine;
using System;

namespace Furion.Tools
{
    /// <summary>
    /// dotnet tool install -g Furion.Tools --add-source ./
    /// dotnet tool install -g furion
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 填充变量
            Arguments.Populate();

            // 监听 Ctrl + C 退出
            Console.CancelKeyPress += (o, e) =>
            {
                Environment.Exit(1);
            };

            // 解析命令（返回命令字典）
            var argumentDictionary = Arguments.Parse().ArgumentDictionary;

            foreach (string key in argumentDictionary.Keys)
            {
                Console.WriteLine(key + "\t\t" + argumentDictionary[key]);
            }

            // 输出版本
            if (argumentDictionary.ContainsKey("v") || argumentDictionary.ContainsKey("version"))
            {
                Console.WriteLine("v0.0.1" + Version);
            }

            // 查看帮助
            if (argumentDictionary.ContainsKey("h") || argumentDictionary.ContainsKey("help"))
            {
                ShowHelps();
            }

            // 没有命令的时候输出
            if (argumentDictionary.Count == 0)
            {
                Console.WriteLine("欢迎使用 Furion.Tools 工具。");
            }
        }

        /// <summary>
        /// 显示帮助文档
        /// </summary>
        private static void ShowHelps()
        {
            var helpAttributes = Arguments.GetArgumentInfo(typeof(Program));

            Console.WriteLine("短参数\t长参数\t描述");
            Console.WriteLine("-----\t----\t--------");

            foreach (var item in helpAttributes)
            {
                var result = item.ShortName + "\t" + item.LongName + "\t" + item.HelpText;
                Console.WriteLine(result);
            }
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
        private static string Help { get; set; }

        /// <summary>
        /// 未匹配的操作符
        /// </summary>
        [Operands]
        private static string[] Operands { get; set; }
    }
}