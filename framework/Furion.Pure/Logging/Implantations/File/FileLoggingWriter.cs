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
/// 文件日志写入器
/// </summary>
internal class FileLoggingWriter
{
    /// <summary>
    /// 文件日志记录器提供程序
    /// </summary>
    private readonly FileLoggerProvider _fileLoggerProvider;

    /// <summary>
    /// 日志配置选项
    /// </summary>
    private readonly FileLoggerOptions _options;

    /// <summary>
    /// 日志文件名
    /// </summary>
    private string _fileName;

    /// <summary>
    /// 文件流
    /// </summary>
    private FileStream _fileStream;

    /// <summary>
    /// 文本写入器
    /// </summary>
    private StreamWriter _textWriter;

    /// <summary>
    /// 缓存上次返回的基本日志文件名，避免重复解析
    /// </summary>
    private string __LastBaseFileName = null;

    /// <summary>
    /// 判断是否启动滚动日志功能
    /// </summary>
    private readonly bool _isEnabledRollingFiles;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileLoggerProvider">文件日志记录器提供程序</param>
    internal FileLoggingWriter(FileLoggerProvider fileLoggerProvider)
    {
        _fileLoggerProvider = fileLoggerProvider;
        _options = fileLoggerProvider.LoggerOptions;
        _isEnabledRollingFiles = _options.MaxRollingFiles > 0 && _options.FileSizeLimitBytes > 0;

        // 解析当前写入日志的文件名
        GetCurrentFileName();

        // 打开文件并持续写入，调用 .Wait() 确保文件流创建完毕
        Task.Run(async () => await OpenFileAsync(_options.Append)).Wait();
    }

    /// <summary>
    /// 获取日志基础文件名
    /// </summary>
    /// <returns>日志文件名</returns>
    private string GetBaseFileName()
    {
        var fileName = _fileLoggerProvider.FileName;

        // 如果配置了日志文件名格式化程序，则先处理再返回
        if (_options.FileNameRule != null)
            fileName = _options.FileNameRule(fileName);

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
        if (_options.FileSizeLimitBytes > 0)
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
            || _options.FileSizeLimitBytes <= 0
            || new FileInfo(baseFileName).Length < _options.FileSizeLimitBytes) return baseFileName;

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
        if (_options.MaxRollingFiles > 0)
        {
            nextFileIndex %= _options.MaxRollingFiles;
        }

        // 返回下一个匹配的日志文件名（完整路径）
        var nextFileName = baseFileNameOnly + (nextFileIndex > 0 ? nextFileIndex.ToString() : "") + Path.GetExtension(baseFileName);
        return Path.Combine(Path.GetDirectoryName(baseFileName), nextFileName);
    }

    /// <summary>
    /// 打开文件
    /// </summary>
    /// <param name="append"></param>
    /// <returns><see cref="Task"/></returns>
    private Task OpenFileAsync(bool append)
    {
        try
        {
            CreateFileStream();
        }
        catch (Exception ex)
        {
            // 处理文件写入错误
            if (_options.HandleWriteError != null)
            {
                var fileWriteError = new FileWriteError(_fileName, ex);
                _options.HandleWriteError(fileWriteError);

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

            // 创建文件流，采用共享锁方式
            _fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, FileOptions.WriteThrough);

            // 删除超出滚动日志限制的文件
            DropFilesIfOverLimit(fileInfo);

            // 判断是否追加还是覆盖
            if (append) _fileStream.Seek(0, SeekOrigin.End);
            else _fileStream.SetLength(0);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 判断是否需要创建新文件写入
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task CheckForNewLogFileAsync()
    {
        var openNewFile = false;
        if (isMaxFileSizeThresholdReached() || isBaseFileNameChanged())
            openNewFile = true;

        // 重新创建新文件并写入
        if (openNewFile)
        {
            await CloseAsync();

            // 计算新文件名
            _fileName = GetNextFileName();

            // 打开新文件并写入
            await OpenFileAsync(false);
        }

        // 是否超出限制的最大大小
        bool isMaxFileSizeThresholdReached() => _options.FileSizeLimitBytes > 0
            && _fileStream.Length > _options.FileSizeLimitBytes;

        // 是否重新自定义了文件名
        bool isBaseFileNameChanged()
        {
            if (_options.FileNameRule != null)
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
    /// 删除超出滚动日志限制的文件
    /// </summary>
    /// <param name="fileInfo"></param>
    private void DropFilesIfOverLimit(FileInfo fileInfo)
    {
        // 判断是否启用滚动文件功能
        if (!_isEnabledRollingFiles) return;

        // 处理 Windows 和 Linux 路径分隔符不一致问题
        var fName = fileInfo.FullName.Replace('\\', '/');

        // 将当前文件名存储到集合中
        var succeed = _fileLoggerProvider._rollingFileNames.TryAdd(fName, fileInfo);

        // 判断超出限制的文件自动删除
        if (succeed && _fileLoggerProvider._rollingFileNames.Count > _options.MaxRollingFiles)
        {
            // 根据最后写入时间删除过时日志
            var dropFiles = _fileLoggerProvider._rollingFileNames
                .OrderBy(u => u.Value.LastWriteTimeUtc)
                .Take(_fileLoggerProvider._rollingFileNames.Count - _options.MaxRollingFiles);

            // 遍历所有需要删除的文件
            foreach (var rollingFile in dropFiles)
            {
                var removeSucceed = _fileLoggerProvider._rollingFileNames.TryRemove(rollingFile.Key, out _);
                if (!removeSucceed) continue;

                // 执行删除
                Task.Run(() =>
                {
                    if (File.Exists(rollingFile.Key)) File.Delete(rollingFile.Key);
                });
            }
        }
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="logMsg">日志消息</param>
    /// <param name="flush"></param>
    /// <returns><see cref="Task"/></returns>
    internal async Task WriteAsync(LogMessage logMsg, bool flush)
    {
        if (_textWriter == null) return;

        try
        {
            await CheckForNewLogFileAsync();
            await _textWriter.WriteLineAsync(logMsg.Message);

            if (flush)
            {
                await _textWriter.FlushAsync();
            }
        }
        catch (Exception ex)
        {
            // 处理文件写入错误
            if (_options.HandleWriteError != null)
            {
                var fileWriteError = new FileWriteError(_fileName, ex);
                _options.HandleWriteError(fileWriteError);
            }
        }
        finally
        {
            logMsg.Context?.Dispose();
        }
    }

    /// <summary>
    /// 关闭文本写入器并释放
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    internal async Task CloseAsync()
    {
        if (_textWriter == null) return;

        var textloWriter = _textWriter;
        _textWriter = null;

        await textloWriter.DisposeAsync();
        await _fileStream.DisposeAsync();

        _fileStream = null;
    }
}