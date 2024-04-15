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

namespace Furion.Schedule;

/// <summary>
/// 作业触发器特性基类
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class TriggerAttribute : Attribute
{
    /// <summary>
    /// 私有开始时间
    /// </summary>
    private string _startTime;

    /// <summary>
    /// 私有结束时间
    /// </summary>
    private string _endTime;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="triggerType">作业触发器类型</param>
    /// <param name="args">作业触发器参数</param>
    public TriggerAttribute(Type triggerType, params object[] args)
    {
        RuntimeTriggerType = triggerType;
        RuntimeTriggerArgs = args;
    }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    public string TriggerId { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 起始时间
    /// </summary>
    public string StartTime
    {
        get { return _startTime; }
        set
        {
            _startTime = value;

            // 解析运行时开始时间
            if (string.IsNullOrWhiteSpace(value)) RuntimeStartTime = null;
            else RuntimeStartTime = Convert.ToDateTime(value);
        }
    }

    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime
    {
        get { return _endTime; }
        set
        {
            _endTime = value;

            // 解析运行时结束时间
            if (string.IsNullOrWhiteSpace(value)) RuntimeEndTime = null;
            else RuntimeEndTime = Convert.ToDateTime(value);
        }
    }

    /// <summary>
    /// 最大触发次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    public long MaxNumberOfRuns { get; set; }

    /// <summary>
    /// 最大出错次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    public long MaxNumberOfErrors { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int NumRetries { get; set; } = 0;

    /// <summary>
    /// 重试间隔时间
    /// </summary>
    /// <remarks>默认1000毫秒</remarks>
    public int RetryTimeout { get; set; } = 1000;

    /// <summary>
    /// 是否立即启动
    /// </summary>
    public bool StartNow { get; set; } = true;

    /// <summary>
    /// 是否启动时执行一次
    /// </summary>
    public bool RunOnStart { get; set; } = false;

    /// <summary>
    /// 是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
    public bool ResetOnlyOnce { get; set; } = true;

    /// <summary>
    /// 作业触发器运行时起始时间
    /// </summary>
    internal DateTime? RuntimeStartTime { get; set; }

    /// <summary>
    /// 作业触发器运行时结束时间
    /// </summary>
    internal DateTime? RuntimeEndTime { get; set; }

    /// <summary>
    /// 作业触发器运行时类型
    /// </summary>
    internal Type RuntimeTriggerType { get; set; }

    /// <summary>
    /// 作业触发器运行时参数
    /// </summary>
    internal object[] RuntimeTriggerArgs { get; set; }
}