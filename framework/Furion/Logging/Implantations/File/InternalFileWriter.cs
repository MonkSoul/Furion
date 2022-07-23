// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Logging;

/// <summary>
/// 内部文件写入器
/// </summary>
internal class InternalFileWriter
{
    /// <summary>
    /// 文件日志记录器提供程序
    /// </summary>
    private readonly FileLoggerProvider _fileLoggerProvider;

    /// <summary>
    /// 日志文件名
    /// </summary>
    private string _fileName;

    /// <summary>
    /// 文件流
    /// </summary>
    private Stream _fileStream;

    /// <summary>
    /// 文本写入器
    /// </summary>
    private TextWriter _textWriter;

    /// <summary>
    /// 缓存上次返回的基本日志文件名，避免重复解析
    /// </summary>
    private string __LastBaseFileName = null;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileLoggerProvider">文件日志记录器提供程序</param>
    internal InternalFileWriter(FileLoggerProvider fileLoggerProvider)
    {
        _fileLoggerProvider = fileLoggerProvider;

        // 解析当前写入日志的文件名
        GetCurrentFileName();

        // 打开文件并持续写入
        OpenFile(_fileLoggerProvider.Append);
    }

    /// <summary>
    /// 获取日志基础文件名
    /// </summary>
    /// <returns>日志文件名</returns>
    private string GetBaseFileName()
    {
        var fileName = _fileLoggerProvider.FileName;

        // 如果配置了日志文件名格式化程序，则先处理再返回
        if (_fileLoggerProvider.FileNameRule != null)
            fileName = _fileLoggerProvider.FileNameRule(fileName);

        return fileName;
    }

    /// <summary>
    /// 解析当前写入日志的文件名
    /// </summary>
    private void GetCurrentFileName()
    {
        // 获取日志基础文件名并将其缓存
        var baseFileName = GetBaseFileName();
        __LastBaseFileName = baseFileName;

        // 是否配置了日志文件最大存储大小
        if (_fileLoggerProvider.FileSizeLimitBytes > 0)
        {
            // 定义文件查找通配符
            var logFileMask = Path.GetFileNameWithoutExtension(baseFileName) + "*" + Path.GetExtension(baseFileName);

            // 获取文件路径
            var logDirName = Path.GetDirectoryName(baseFileName);

            // 如果没有配置文件路径则默认放置根目录
            if (string.IsNullOrEmpty(logDirName)) logDirName = Directory.GetCurrentDirectory();

            // 在当前目录下根据文件通配符查找所有匹配的文件
            var logFiles = Directory.Exists(logDirName)
                ? Directory.GetFiles(logDirName, logFileMask, SearchOption.TopDirectoryOnly)
                : Array.Empty<string>();

            // 处理已有日志文件存在情况
            if (logFiles.Length > 0)
            {
                // 根据文件名和最后更新时间获取最近操作的文件
                var lastFileInfo = logFiles
                        .Select(fName => new FileInfo(fName))
                        .OrderByDescending(fInfo => fInfo.Name)
                        .OrderByDescending(fInfo => fInfo.LastWriteTime).First();

                _fileName = lastFileInfo.FullName;
            }
            // 没有任何匹配的日志文件直接使用当前基础文件名
            else _fileName = baseFileName;
        }
        else _fileName = baseFileName;
    }

    /// <summary>
    /// 获取下一个匹配的日志文件名
    /// </summary>
    /// <remarks>只有配置了 <see cref="FileLoggerOptions.FileSizeLimitBytes"/> 或 <see cref="FileLoggerOptions.FileNameRule"/> 或 <see cref="FileLoggerOptions.MaxRollingFiles"/> 有效</remarks>
    /// <returns>新的文件名</returns>
    private string GetNextFileName()
    {
        // 获取日志基础文件名
        var baseFileName = GetBaseFileName();

        // 如果文件不存在或没有达到 FileSizeLimitBytes 限制大小，则返回基础文件名
        if (!System.IO.File.Exists(baseFileName)
            || _fileLoggerProvider.FileSizeLimitBytes <= 0
            || new FileInfo(baseFileName).Length < _fileLoggerProvider.FileSizeLimitBytes) return baseFileName;

        // 获取日志基础文件名和当前日志文件名
        var currentFileIndex = 0;
        var baseFileNameOnly = Path.GetFileNameWithoutExtension(baseFileName);
        var currentFileNameOnly = Path.GetFileNameWithoutExtension(_fileName);

        // 解析日志文件名【递增】部分
        var suffix = currentFileNameOnly[baseFileNameOnly.Length..];
        if (suffix.Length > 0 && int.TryParse(suffix, out var parsedIndex))
        {
            currentFileIndex = parsedIndex;
        }

        // 【递增】部分 +1
        var nextFileIndex = currentFileIndex + 1;

        // 如果配置了最大【递增】数，则超出自动从头开始（覆盖写入）
        if (_fileLoggerProvider.MaxRollingFiles > 0)
        {
            nextFileIndex %= _fileLoggerProvider.MaxRollingFiles;
        }

        // 返回下一个匹配的日志文件名（完整路径）
        var nextFileName = baseFileNameOnly + (nextFileIndex > 0 ? nextFileIndex.ToString() : "") + Path.GetExtension(baseFileName);
        return Path.Combine(Path.GetDirectoryName(baseFileName), nextFileName);
    }

    /// <summary>
    /// 打开文件
    /// </summary>
    /// <param name="append"></param>
    private void OpenFile(bool append)
    {
        try
        {
            CreateFileStream();
        }
        catch (Exception ex)
        {
            // 处理文件写入错误
            if (_fileLoggerProvider.HandleWriteError != null)
            {
                var fileWriteError = new FileWriteError(_fileName, ex);
                _fileLoggerProvider.HandleWriteError(fileWriteError);

                // 如果配置了备用文件名，则重新写入
                if (fileWriteError.RollbackFileName != null)
                {
                    _fileLoggerProvider.FileName = fileWriteError.RollbackFileName;

                    // 递归操作，直到应用程序停止
                    GetCurrentFileName();
                    CreateFileStream();
                }
            }
            // 其他直接抛出异常
            else throw;
        }

        // 初始化文本写入器
        _textWriter = new StreamWriter(_fileStream);

        // 创建文件流
        void CreateFileStream()
        {
            var fileInfo = new FileInfo(_fileName);

            // 判断文件目录是否存在，不存在则自动创建
            fileInfo.Directory.Create();

            // 创建文件流
            _fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write);

            // 判断是否追加还是覆盖
            if (append) _fileStream.Seek(0, SeekOrigin.End);
            else _fileStream.SetLength(0);
        }
    }

    /// <summary>
    /// 判断是否需要创建新文件写入
    /// </summary>
    private void CheckForNewLogFile()
    {
        var openNewFile = false;
        if (isMaxFileSizeThresholdReached() || isBaseFileNameChanged())
            openNewFile = true;

        // 重新创建新文件并写入
        if (openNewFile)
        {
            Close();

            // 计算新文件名
            _fileName = GetNextFileName();

            // 打开新文件并写入
            OpenFile(false);
        }

        // 是否超出限制的最大大小
        bool isMaxFileSizeThresholdReached() => _fileLoggerProvider.FileSizeLimitBytes > 0
            && _fileStream.Length > _fileLoggerProvider.FileSizeLimitBytes;

        // 是否重新自定义了文件名
        bool isBaseFileNameChanged()
        {
            if (_fileLoggerProvider.FileNameRule != null)
            {
                var baseFileName = GetBaseFileName();

                if (baseFileName != __LastBaseFileName)
                {
                    __LastBaseFileName = baseFileName;
                    return true;
                }

                return false;
            }

            return false;
        }
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="flush"></param>
    internal void Write(string message, bool flush)
    {
        if (_textWriter != null)
        {
            CheckForNewLogFile();
            _textWriter.WriteLine(message);

            if (flush) _textWriter.Flush();
        }
    }

    /// <summary>
    /// 关闭文本写入器并释放
    /// </summary>
    internal void Close()
    {
        if (_textWriter != null)
        {
            var textloWriter = _textWriter;
            _textWriter = null;

            textloWriter.Dispose();
            _fileStream.Dispose();

            _fileStream = null;
        }
    }
}