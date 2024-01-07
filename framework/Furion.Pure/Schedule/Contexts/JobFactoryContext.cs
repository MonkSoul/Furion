// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业处理程序工厂上下文
/// </summary>
[SuppressSniffer]
public sealed class JobFactoryContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="jobType">作业类型</param>
    public JobFactoryContext(string jobId, Type jobType)
    {
        JobId = jobId;
        JobType = jobType;
    }

    /// <summary>
    /// 作业类型
    /// </summary>
    public Type JobType { get; }

    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; }
}