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
/// 取消作业执行 Token 器
/// </summary>
public interface IJobCancellationToken
{
    /// <summary>
    /// 获取或创建取消作业执行 Token
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="runId">作业触发器触发的唯一标识</param>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="CancellationToken"/></returns>
    CancellationTokenSource GetOrCreate(string jobId, string runId, CancellationToken stoppingToken);

    /// <summary>
    /// 取消（完成）正在执行的执行
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="outputLog">是否显示日志</param>
    void Cancel(string jobId, string triggerId = null, bool outputLog = true);
}