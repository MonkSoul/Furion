// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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