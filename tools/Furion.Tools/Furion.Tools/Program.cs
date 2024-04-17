// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.Tools.CommandLine;
using System.ComponentModel.DataAnnotations;

namespace Furion.Tools;

internal class Program
{
    // 初始化并自动填充命令参数
    private static void Main(string[] args) => Cli.Inject();

    /// <summary>
    /// 版本号
    /// </summary>
    [Argument('v', "version", "版本号")]
    private static string Version { get; set; }

    private static void VersionHandler(ArgumentMetadata argument) => Cli.Success(Cli.GetVersion());

    /// <summary>
    /// 查看帮助
    /// </summary>
    [Argument('h', "help", "使用帮助")]
    private static bool Help { get; set; }

    private static void HelpHandler(ArgumentMetadata argument) => Cli.GetHelpText("furion");

    /// <summary>
    /// 选择框架
    /// </summary>
    [Argument('f', "framework", "选择喜欢的框架")]
    private static bool Framework { get; set; }

    private static void FrameworkHandler(ArgumentMetadata argument) => Cli.WriteLine(Cli.ReadOptions("请选择您最喜欢的框架：", new[] { "Furion", "Abp Next", "ASP.NET Core" }));

    [Argument('n', "name", "告诉我你的名字"), MaxLength(3, ErrorMessage = "名字不能超过 3 个字符")]
    private static string Name { get; set; }

    private static void NameHandler(ArgumentMetadata argument) => Cli.WriteLine($"Hello, {Name}");

    /// <summary>
    /// 操作符
    /// </summary>
    [Operands]
    private static string[] Operands { get; set; }

    private static void NoMatchesHandler(bool isEmpty, string[] operands, Dictionary<string, object> noMatches)
    {
        if (isEmpty) Cli.WriteLine($"欢迎使用 {Cli.GetDescription()}");
        if (operands.Length > 0) Cli.Error($"未找到该操作符：{string.Join(",", operands)}");
        if (noMatches.Count > 0) Cli.Error($"未找到该参数：{string.Join(",", noMatches.Keys)}");
    }
}