// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Diagnostics;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     带读写进度的文件流
/// </summary>
internal sealed class ProgressFileStream : Stream
{
    /// <summary>
    ///     文件大小
    /// </summary>
    internal readonly long _fileLength;

    /// <inheritdoc cref="Stream" />
    internal readonly Stream _fileStream;

    /// <inheritdoc cref="FileTransferProgress" />
    internal readonly FileTransferProgress _fileTransferProgress;

    /// <summary>
    ///     文件传输进度信息的通道
    /// </summary>
    internal readonly Channel<FileTransferProgress> _progressChannel;

    /// <inheritdoc cref="Stopwatch" />
    internal readonly Stopwatch _stopwatch;

    /// <summary>
    ///     已传输的数据量
    /// </summary>
    internal long _transferred;

    /// <summary>
    ///     <inheritdoc cref="ProgressFileStream" />
    /// </summary>
    /// <param name="fileStream">
    ///     <see cref="Stream" />
    /// </param>
    /// <param name="fileFullName">文件完整路径或文件名</param>
    /// <param name="fileLength">文件大小</param>
    /// <param name="progressChannel">文件传输进度信息的通道</param>
    internal ProgressFileStream(Stream fileStream, string fileFullName, long fileLength,
        Channel<FileTransferProgress> progressChannel)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fileStream);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileFullName);
        ArgumentNullException.ThrowIfNull(progressChannel);

        _fileStream = fileStream;
        _fileLength = fileLength;
        _progressChannel = progressChannel;

        // 初始化 FileTransferProgress 实例
        _fileTransferProgress = new FileTransferProgress(fileFullName, _fileLength);

        // 初始化 Stopwatch 实例并开启计时操作
        _stopwatch = Stopwatch.StartNew();
    }

    /// <inheritdoc />
    public override bool CanRead => _fileStream.CanRead;

    /// <inheritdoc />
    public override bool CanSeek => _fileStream.CanSeek;

    /// <inheritdoc />
    public override bool CanWrite => _fileStream.CanWrite;

    /// <inheritdoc />
    public override bool CanTimeout => _fileStream.CanTimeout;

    /// <inheritdoc />
    public override long Length => _fileLength;

    /// <inheritdoc />
    public override long Position
    {
        get => _fileStream.Position;
        set => _fileStream.Position = value;
    }

    /// <inheritdoc />
    public override void Flush() => _fileStream.Flush();

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        // 从文件流读取数据到缓冲区
        var bytesRead = _fileStream.Read(buffer, offset, count);

        // 报告进度
        if (bytesRead > 0)
        {
            ReportProgress(bytesRead);
        }

        return bytesRead;
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) => _fileStream.Seek(offset, origin);

    /// <inheritdoc />
    public override void SetLength(long value) => _fileStream.SetLength(value);

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        // 向文件流写入数据
        _fileStream.Write(buffer, offset, count);

        // 报告进度
        ReportProgress(count);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        // 释放托管资源
        if (disposing)
        {
            _fileStream.Dispose();
            _stopwatch.Stop();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    ///     报告进度
    /// </summary>
    /// <param name="increment">增加的数据量</param>
    internal void ReportProgress(int increment)
    {
        // 更新当前已传输的数据量
        _transferred += increment;
        _fileTransferProgress.UpdateProgress(_transferred, _stopwatch.Elapsed);

        // 发送文件传输进度到通道
        _progressChannel.Writer.TryWrite(_fileTransferProgress);
    }
}