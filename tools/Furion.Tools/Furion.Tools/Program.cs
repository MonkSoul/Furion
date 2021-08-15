using Furion.Tools.CommandLine;

namespace Furion.Tools
{
    /// <summary>
    /// 本地安装必须找到 .nupkg 所在目录
    /// 本地测试：dotnet tool install -g Furion.Tools --add-source ./
    /// 全局安装：dotnet tool install -g Furion.Tools
    /// 卸载工具：dotnet tool uninstall -g Furion.Tools
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 查看版本
            Cli.Check(nameof(Version), u => Cli.Success(Cli.GetVersion()));

            // 查看帮助
            Cli.Check(nameof(Help), u => Cli.GetHelpText("furion"));

            // 选择框架
            Cli.Check(nameof(Framework), u => Cli.WriteLine(Cli.ReadOptions("请选择您最喜欢的框架：", new[] { "Furion", "Abp Next", "ASP.NET Core" })));

            // 没有匹配输出
            Cli.CheckNoMatch((empty, operands, noMatches) =>
            {
                if (empty) Cli.WriteLine($"欢迎使用 {Cli.GetDescription()}");
                if (operands.Length > 0) Cli.Error($"未找到该操作符：{string.Join(",", operands)}");
                if (noMatches.Count > 0) Cli.Error($"未找到该参数：{string.Join(",", noMatches.Keys)}");
            });
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
        /// 操作符
        /// </summary>
        [Operands]
        private static string[] Operands { get; set; }
    }
}