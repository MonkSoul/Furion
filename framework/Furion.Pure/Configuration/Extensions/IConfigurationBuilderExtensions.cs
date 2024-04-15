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