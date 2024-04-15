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

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Profiling;
using StackExchange.Profiling.Internal;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 监听 EFCore 操作进程
/// </summary>
internal class RelationalDiagnosticListener : IMiniProfilerDiagnosticListener
{
    /// <summary>
    /// 监听进程名
    /// </summary>
    public string ListenerName => "Microsoft.EntityFrameworkCore";

    /// <summary>
    /// 操作命令集合
    /// </summary>
    private readonly ConcurrentDictionary<Guid, CustomTiming>
        _commands = new(),
        _opening = new(),
        _closing = new();

    private readonly ConcurrentDictionary<Guid, CustomTiming>
        _readers = new();

    /// <summary>
    /// 操作完成监听
    /// </summary>
    public void OnCompleted()
    { }

    /// <summary>
    /// 操作错误监听
    /// </summary>
    /// <param name="error"></param>
    public void OnError(Exception error)
    {
        Trace.WriteLine(error);
    }

    /// <summary>
    /// 操作过程监听
    /// </summary>
    /// <param name="kv"></param>
    public void OnNext(KeyValuePair<string, object> kv)
    {
        if (!App.CanBeMiniProfiler()) return;

        var key = kv.Key;
        var val = kv.Value;

        // 监听命令执行前
        if (key == RelationalEventId.CommandExecuting.Name)
        {
            if (val is CommandEventData data)
            {
                var timing = data.Command.GetTiming(data.ExecuteMethod + (data.IsAsync ? " (Async)" : null), MiniProfiler.Current);
                if (timing != null)
                {
                    _commands[data.CommandId] = timing;
                }
            }
        }
        // 监听命令执行完成
        else if (key == RelationalEventId.CommandExecuted.Name)
        {
            if (val is CommandExecutedEventData data && _commands.TryRemove(data.CommandId, out var current))
            {
                if (data.Result is RelationalDataReader)
                {
                    _readers[data.CommandId] = current;
                    current.FirstFetchCompleted();
                }
                else
                {
                    current.Stop();
                }
            }
        }
        // 监听命令执行异常
        else if (key == RelationalEventId.CommandError.Name)
        {
            if (val is CommandErrorEventData data && _commands.TryRemove(data.CommandId, out var command))
            {
                command.Errored = true;
                command.Stop();
            }
        }
        // 监听读取数据释放事件
        else if (key == RelationalEventId.DataReaderDisposing.Name)
        {
            if (val is DataReaderDisposingEventData data && _readers.TryRemove(data.CommandId, out var reader))
            {
                reader.Stop();
            }
        }
        // 监听连接事件
        else if (key == RelationalEventId.ConnectionOpening.Name)
        {
            var profiler = MiniProfiler.Current;
            if (val is ConnectionEventData data && (profiler?.Options == null || profiler.Options.TrackConnectionOpenClose))
            {
                var timing = profiler.CustomTiming("sql",
                    data.IsAsync ? "Connection OpenAsync()" : "Connection Open()",
                    data.IsAsync ? "OpenAsync" : "Open");
                if (timing != null)
                {
                    _opening[data.ConnectionId] = timing;
                }
            }
        }
        // 监听连接完成事件
        else if (key == RelationalEventId.ConnectionOpened.Name)
        {
            if (val is ConnectionEndEventData data && _opening.TryRemove(data.ConnectionId, out var openingTiming))
            {
                openingTiming?.Stop();
            }
        }
        // 监听连接关闭事件
        else if (key == RelationalEventId.ConnectionClosing.Name)
        {
            var profiler = MiniProfiler.Current;
            if (val is ConnectionEventData data && (profiler?.Options == null || profiler.Options.TrackConnectionOpenClose))
            {
                var timing = profiler.CustomTiming("sql",
                    data.IsAsync ? "Connection CloseAsync()" : "Connection Close()",
                    data.IsAsync ? "CloseAsync" : "Close");
                if (timing != null)
                {
                    _closing[data.ConnectionId] = timing;
                }
            }
        }
        // 监听连接关闭完成事件
        else if (key == RelationalEventId.ConnectionClosed.Name)
        {
            if (val is ConnectionEndEventData data && _closing.TryRemove(data.ConnectionId, out var closingTiming))
            {
                closingTiming?.Stop();
            }
        }
        // 监听连接异常事件
        else if (key == RelationalEventId.ConnectionError.Name)
        {
            if (val is ConnectionErrorEventData data)
            {
                if (_opening.TryRemove(data.ConnectionId, out var openingTiming))
                {
                    openingTiming.Errored = true;
                }
                if (_closing.TryRemove(data.ConnectionId, out var closingTiming))
                {
                    closingTiming.Errored = true;
                }
            }
        }
    }
}