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

using System.Text;
using System.Text.RegularExpressions;

namespace Furion.Templates;

/// <summary>
/// 模板静态类
/// </summary>
[SuppressSniffer]
public static class TP
{
    /// <summary>
    /// 模板正则表达式对象
    /// </summary>
    private static readonly Lazy<Regex> _lazyRegex = new(() => new(@"^##(?<prop>.*)?##[:：]?\s*(?<content>[\s\S]*)"));

    /// <summary>
    /// 生成规范日志模板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="description">描述</param>
    /// <param name="items">列表项，如果以 ##xxx## 开头，自动生成 xxx: 属性</param>
    /// <returns><see cref="string"/></returns>
    public static string Wrapper(string title, string description, params string[] items)
    {
        // 处理不同编码问题
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"┏━━━━━━━━━━━  {title} ━━━━━━━━━━━").AppendLine();

        // 添加描述
        if (!string.IsNullOrWhiteSpace(description))
        {
            stringBuilder.Append($"┣ {description}").AppendLine().Append("┣ ").AppendLine();
        }

        // 添加项
        if (items != null && items.Length > 0)
        {
            var propMaxLength = items.Where(u => _lazyRegex.Value.IsMatch(u))
                .DefaultIfEmpty(string.Empty)
                .Max(u => _lazyRegex.Value.Match(u).Groups["prop"].Value.Length);

            // 控制项名称对齐空白占位数
            propMaxLength += (propMaxLength >= 5 ? 10 : 5);

            // 遍历每一项并进行正则表达式匹配
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];

                // 判断是否匹配 ##xxx##
                if (_lazyRegex.Value.IsMatch(item))
                {
                    var match = _lazyRegex.Value.Match(item);
                    var prop = match.Groups["prop"].Value;
                    var content = match.Groups["content"].Value;

                    var propTitle = $"{prop}：";
                    stringBuilder.Append($"┣ {PadRight(propTitle, propMaxLength)}{content}").AppendLine();
                }
                else
                {
                    stringBuilder.Append($"┣ {item}").AppendLine();
                }
            }
        }

        stringBuilder.Append($"┗━━━━━━━━━━━  {title} ━━━━━━━━━━━");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 矩形包裹
    /// </summary>
    /// <param name="lines">多行消息</param>
    /// <param name="align">对齐方式，-1/左对齐；0/居中对其；1/右对齐</param>
    /// <param name="pad">间隙</param>
    /// <returns><see cref="string"/></returns>
    public static string WrapperRectangle(string[] lines, int align = 0, int pad = 20)
    {
        // 处理不同编码问题
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // 计算矩形框的宽度，取所有字符串中最长的长度，再乘以 2
        var width = lines.Max(GetLength) + pad;

        var stringBuilder = new StringBuilder();

        // 添加矩形框的上边框
        stringBuilder.AppendLine("+" + new string('-', width - 2) + "+");

        var row = 0;
        foreach (var line in lines)
        {
            // 当前字符串的长度
            var len = GetLength(line);
            var padding = align switch
            {
                -1 => 2,
                0 => (width - len - 2) / 2,
                1 => (width - len - 2) - 2,
                _ => 2
            };

            // 添加当前字符串前的空格，实现居中显示
            stringBuilder.Append("|" + new string(' ', padding));

            // 添加当前字符串
            stringBuilder.Append(line);

            // 添加当前字符串后的空格，实现等宽
            stringBuilder.Append(new string(' ', width - len - 2 - padding) + "|");

            // 添加换行符
            stringBuilder.AppendLine();

            // 更新当前行数
            row++;
        }

        // 添加矩形框的下边框
        stringBuilder.Append("+" + new string('-', width - 2) + "+");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// 等宽文字对齐
    /// </summary>
    /// <param name="str"></param>
    /// <param name="totalByteCount"></param>
    /// <returns></returns>
    private static string PadRight(string str, int totalByteCount)
    {
        var coding = Encoding.GetEncoding("gbk");
        var dcount = 0;

        foreach (var character in str.ToCharArray())
        {
            if (coding.GetByteCount(character.ToString()) == 2)
                dcount++;
        }

        var w = str.PadRight(totalByteCount - dcount);
        return w;
    }

    /// <summary>
    /// 获取字符串长度
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>字符串长度</returns>
    public static int GetLength(string str)
    {
        var coding = Encoding.GetEncoding("gbk");
        return coding.GetByteCount(str);
    }
}