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

namespace Furion.Logging;

/// <summary>
/// 文件写入错误信息上下文
/// </summary>
[SuppressSniffer]
public sealed class FileWriteError
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="currentFileName">当前日志文件名</param>
    /// <param name="exception">异常对象</param>
    internal FileWriteError(string currentFileName, Exception exception)
    {
        CurrentFileName = currentFileName;
        Exception = exception;
    }

    /// <summary>
    /// 当前日志文件名
    /// </summary>
    public string CurrentFileName { get; private set; }

    /// <summary>
    /// 引起文件写入异常信息
    /// </summary>
    public Exception Exception { get; private set; }

    /// <summary>
    /// 备用日志文件名
    /// </summary>
    internal string RollbackFileName { get; private set; }

    /// <summary>
    /// 配置日志文件写入错误后新的备用日志文件名
    /// </summary>
    /// <param name="rollbackFileName">备用日志文件名</param>
    public void UseRollbackFileName(string rollbackFileName)
    {
        RollbackFileName = rollbackFileName;
    }
}