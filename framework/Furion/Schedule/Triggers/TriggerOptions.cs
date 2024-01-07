// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业触发器配置选项
/// </summary>
[SuppressSniffer]
public sealed class TriggerOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    internal TriggerOptions()
    {
    }

    /// <summary>
    /// 重写 <see cref="ConvertToSQL"/>
    /// </summary>
    public Func<string, string[], Trigger, PersistenceBehavior, NamingConventions, string> ConvertToSQL
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
    /// <see cref="ConvertToSQL"/> 静态配置
    /// </summary>
    internal static Func<string, string[], Trigger, PersistenceBehavior, NamingConventions, string> ConvertToSQLConfigure { get; private set; }
}