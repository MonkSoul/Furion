// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// 配置表名称前后缀
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TableFixsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    public TableFixsAttribute(string prefix)
    {
        Prefix = prefix;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    public TableFixsAttribute(string prefix, string suffix)
        : this(prefix)
    {
        Suffix = suffix;
    }

    /// <summary>
    /// 前缀
    /// </summary>
    /// <remarks>前缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Prefix { get; set; }

    /// <summary>
    /// 后缀
    /// </summary>
    /// <remarks>后缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Suffix { get; set; }
}