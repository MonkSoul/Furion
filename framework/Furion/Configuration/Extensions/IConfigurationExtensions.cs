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

namespace Microsoft.Extensions.Configuration;

/// <summary>
/// IConfiguration 接口拓展
/// </summary>
[SuppressSniffer]
public static class IConfigurationExtensions
{
    /// <summary>
    /// 判断配置节点是否存在
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <returns>是否存在</returns>
    public static bool Exists(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Exists();
    }

    /// <summary>
    /// 获取配置节点并转换成指定类型
    /// </summary>
    /// <typeparam name="T">节点类型</typeparam>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <returns>节点类型实例</returns>
    public static T Get<T>(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// 获取配置节点并转换成指定类型
    /// </summary>
    /// <typeparam name="T">节点类型</typeparam>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="configureOptions">配置值绑定到指定类型额外配置</param>
    /// <returns>节点类型实例</returns>
    public static T Get<T>(this IConfiguration configuration
        , string key
        , Action<BinderOptions> configureOptions)
    {
        return configuration.GetSection(key).Get<T>(configureOptions);
    }

    /// <summary>
    /// 获取节点配置
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="type">节点类型</param>
    /// <returns><see cref="object"/> 实例</returns>
    public static object Get(this IConfiguration configuration
        , string key
        , Type type)
    {
        return configuration.GetSection(key).Get(type);
    }

    /// <summary>
    /// 获取节点配置
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="type">节点类型</param>
    /// <param name="configureOptions">配置值绑定到指定类型额外配置</param>
    /// <returns><see cref="object"/> 实例</returns>
    public static object Get(this IConfiguration configuration
        , string key
        , Type type
        , Action<BinderOptions> configureOptions)
    {
        return configuration.GetSection(key).Get(type, configureOptions);
    }
}