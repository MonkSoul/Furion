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

using Furion.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace Microsoft.Extensions.Configuration;

/// <summary>
/// IConfigurationBuilder 接口拓展
/// </summary>
[SuppressSniffer]
public static class IConfigurationBuilderExtensions
{
    /// <summary>
    /// 添加配置文件
    /// </summary>
    /// <param name="configurationBuilder">配置构建对象</param>
    /// <param name="fileName">文件名</param>
    /// <param name="environment">环境对象</param>
    /// <param name="optional">可选文件，设置 true 跳过文件存在检查</param>
    /// <param name="reloadOnChange">是否监听文件更改</param>
    /// <param name="includeEnvironment">是否包含环境文件格式注册</param>
    /// <returns>配置构建对象</returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="InvalidOperationException" />
    public static IConfigurationBuilder AddFile(this IConfigurationBuilder configurationBuilder
        , string fileName
        , IHostEnvironment environment = default
        , bool optional = true
        , bool reloadOnChange = false
        , bool includeEnvironment = false)
    {
        // 检查文件名格式
        CheckFileNamePattern(fileName
            , out var fileNamePart
            , out var environmentNamePart
            , out var fileNameWithEnvironmentPart
            , out var parameterPart);

        // 获取文件名绝对路径
        var filePath = ResolveRealAbsolutePath(fileNamePart);

        // 填充配置参数
        if (parameterPart.Count > 0)
        {
            TrySetParameter(parameterPart
                , nameof(optional)
                , ref optional);
            TrySetParameter(parameterPart
                , nameof(reloadOnChange)
                , ref reloadOnChange);
            TrySetParameter(parameterPart
                , nameof(includeEnvironment)
                , ref includeEnvironment);
        }

        // 添加配置文件
        configurationBuilder.Add(CreateFileConfigurationSource(filePath
            , optional
            , reloadOnChange));

        // 处理包含环境标识的文件
        if (environment is not null
            && includeEnvironment
            && !environment.EnvironmentName.Equals(environmentNamePart, StringComparison.OrdinalIgnoreCase))
        {
            // 取得带环境文件名绝对路径
            var fileNameWithEnvironmentPath = ResolveRealAbsolutePath(fileNameWithEnvironmentPart.Replace("{env}", environment.EnvironmentName));

            // 添加带环境配置文件
            configurationBuilder.Add(CreateFileConfigurationSource(fileNameWithEnvironmentPath
                , optional
                , reloadOnChange));
        }

        return configurationBuilder;
    }

    /// <summary>
    /// 检查文件名格式是否是受支持的格式
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="fileNamePart">返回文件名匹配部分</param>
    /// <param name="environmentNamePart">环境名匹配部分</param>
    /// <param name="fileNameWithEnvironmentPart">带环境标识的文件名</param>
    /// <param name="parameterPart">参数匹配部分</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="InvalidOperationException" />
    private static void CheckFileNamePattern(string fileName
        , out string fileNamePart
        , out string environmentNamePart
        , out string fileNameWithEnvironmentPart
        , out IDictionary<string, bool> parameterPart)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        // 检查文件名格式
        if (!Regex.IsMatch(fileName
            , Constants.Patterns.ConfigurationFileName
            , RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
        {
            throw new InvalidOperationException($"The <{fileName}> is not a valid supported file name format.");
        }

        // 匹配文件名部分
        var fileNameMatch = Regex.Match(fileName
            , Constants.Patterns.ConfigurationFileName
            , RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        fileNamePart = fileNameMatch.Groups["fileName"].Value;
        // 取环境名
        environmentNamePart = fileNameMatch.Groups["environmentName"].Value;

        // 生成带环境标识的文件名
        var realName = fileNameMatch.Groups["realName"].Value;
        var extension = fileNameMatch.Groups["extension"].Value;
        fileNameWithEnvironmentPart = $"{realName}.{{env}}{extension}";

        // 匹配文件名参数部分
        parameterPart = Regex.Matches(fileName
            , Constants.Patterns.ConfigurationFileParameter
            , RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace)
            .ToDictionary(u => u.Groups["parameter"].Value, u => bool.Parse(u.Groups["value"].Value));
    }

    /// <summary>
    /// 分析配置文件名并返回真实绝对路径
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns>返回文件绝对路径</returns>
    private static string ResolveRealAbsolutePath(string fileName)
    {
        // 获取文件名首个字符
        var firstChar = fileName[0];

        // 如果文件名包含 : 符号，则认为是一个绝对路径，针对 windows 系统路径
        if (fileName.IndexOf(':') > -1
            && firstChar != '/'
            && firstChar != '!')
        {
            return fileName;
        }

        // 拼接绝对路径
        return firstChar switch
        {
            '&' or '.' => Path.Combine(AppContext.BaseDirectory, fileName[1..]),
            '/' or '!' => fileName[1..],
            '@' or '~' => Path.Combine(Directory.GetCurrentDirectory(), fileName[1..]),
            _ => Path.Combine(Directory.GetCurrentDirectory(), fileName)
        };
    }

    /// <summary>
    /// 根据文件路径创建文件配置源
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="optional">可选文件，设置 true 跳过文件存在检查</param>
    /// <param name="reloadOnChange">是否监听文件更改</param>
    /// <returns><see cref="FileConfigurationSource"/> 实例</returns>
    /// <exception cref="InvalidOperationException" />
    private static FileConfigurationSource CreateFileConfigurationSource(string filePath
        , bool optional = true
        , bool reloadOnChange = false)
    {
        // 获取文件拓展名
        var fileExtension = Path.GetExtension(filePath).ToLower();

        // 创建受支持的文件配置源实例，仅支持 .json/.xml/.ini 拓展名
        FileConfigurationSource fileConfigurationSource = fileExtension switch
        {
            ".json" => new JsonConfigurationSource { Path = filePath, Optional = optional, ReloadOnChange = reloadOnChange },
            ".xml" => new XmlConfigurationSource { Path = filePath, Optional = optional, ReloadOnChange = reloadOnChange },
            ".ini" => new IniConfigurationSource { Path = filePath, Optional = optional, ReloadOnChange = reloadOnChange },
            _ => throw new InvalidOperationException($"Cannot create a file <{fileExtension}> configuration source for this file type.")
        };

        // 根据文件配置源解析对应文件配置提供程序
        fileConfigurationSource.ResolveFileProvider();

        return fileConfigurationSource;
    }

    /// <summary>
    /// 设置 FileConfigurationSouce 参数
    /// </summary>
    /// <param name="parameters">字典参数结合</param>
    /// <param name="parameterName">参数名</param>
    /// <param name="value">参数值</param>
    private static void TrySetParameter(IDictionary<string, bool> parameters
        , string parameterName
        , ref bool value)
    {
        // 只有包含该参数才改变值
        if (parameters.ContainsKey(parameterName))
        {
            value = parameters[parameterName];
        }
    }
}