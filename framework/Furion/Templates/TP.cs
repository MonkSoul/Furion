// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Text;
using System.Text.RegularExpressions;

namespace Furion.Templates;

/// <summary>
/// 模板静态类
/// </summary>
public static class TP
{
    /// <summary>
    /// 生成规范日志模板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="description">描述</param>
    /// <param name="items">列表项，如果以 [xxx] 开头，自动生成 xxx: 属性</param>
    /// <returns><see cref="string"/></returns>
    public static string Wrapper(string title, string description, params string[] items)
    {
        var regex = new Regex(@"^\[(?<prop>.*)?\][:：]?\s*(?<content>.*)");

        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"┏━━━━━━━━━━━  {title} ━━━━━━━━━━━").AppendLine();

        // 添加描述
        if (!string.IsNullOrWhiteSpace(description))
        {
            stringBuilder.Append($"┣ {description}").AppendLine().Append("┣ ").AppendLine();
        }

        if (items != null && items.Length > 0)
        {
            var propMaxLength = items.Where(u => regex.IsMatch(u))
                .DefaultIfEmpty(string.Empty)
                .Max(u => regex.Match(u).Groups["prop"].Value.Length) + 5;

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (regex.IsMatch(item))
                {
                    var match = regex.Match(item);
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

        stringBuilder.Append($"┗━━━━━━━━━━━  {title} ━━━━━━━━━━━").AppendLine();
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
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var coding = Encoding.GetEncoding("gbk");
        var dcount = 0;
        foreach (var ch in str.ToCharArray())
        {
            if (coding.GetByteCount(ch.ToString()) == 2)
                dcount++;
        }
        var w = str.PadRight(totalByteCount - dcount);
        return w;
    }
}