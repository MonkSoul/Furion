// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业信息配置选项
/// </summary>
[SuppressSniffer]
public sealed class JobDetailOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    internal JobDetailOptions()
    {
    }

    /// <summary>
    /// 重写 <see cref="ConvertToSQL"/>
    /// </summary>
    public Func<string, string[], JobDetail, PersistenceBehavior, NamingConventions, string> ConvertToSQL
    {
        get
        {
            return ConvertToSQLConfigure;
        }
        set
        {
            ConvertToSQLConfigure = value;
        }
    }

    /// <summary>
    /// 启用作业执行详细日志
    /// </summary>
    public bool LogEnabled
    {
        get
        {
            return InternalLogEnabled;
        }
        set
        {
            InternalLogEnabled = value;
        }
    }

    /// <summary>
    /// <see cref="LogEnabled"/> 静态配置
    /// </summary>
    internal static bool InternalLogEnabled { get; private set; }

    /// <summary>
    /// <see cref="ConvertToSQL"/> 静态配置
    /// </summary>
    internal static Func<string, string[], JobDetail, PersistenceBehavior, NamingConventions, string> ConvertToSQLConfigure { get; private set; }
}