// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;

namespace Microsoft.Extensions.Configuration;

/// <summary>
/// <see cref="IConfiguration"/> 拓展
/// </summary>
public static class IConfigurationExtenstions
{
    /// <summary>
    /// 刷新配置对象
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IConfiguration Reload(this IConfiguration configuration)
    {
        if (App.RootServices == null) return configuration;

        var newConfiguration = App.GetService<IConfiguration>(App.RootServices);
        InternalApp.Configuration = newConfiguration;

        return newConfiguration;
    }
}