// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 命名转换器
/// </summary>
/// <remarks>用于生成持久化 SQL 语句</remarks>
[SuppressSniffer]
public enum NamingConventions
{
    /// <summary>
    /// 驼峰命名法
    /// </summary>
    /// <remarks>第一个单词首字母小写</remarks>
    CamelCase = 0,

    /// <summary>
    /// 帕斯卡命名法
    /// </summary>
    /// <remarks>每一个单词首字母大写</remarks>
    Pascal = 1,

    /// <summary>
    /// 下划线命名法
    /// </summary>
    /// <remarks>每次单词使用下划线连接且首字母都是小写</remarks>
    UnderScoreCase = 2
}