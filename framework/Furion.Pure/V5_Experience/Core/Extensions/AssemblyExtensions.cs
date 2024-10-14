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

using System.Reflection;

namespace Furion.Extensions;

/// <summary>
///     <see cref="Assembly" /> 拓展类
/// </summary>
internal static class AssemblyExtensions
{
    /// <summary>
    ///     获取所有类型
    /// </summary>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    /// <param name="exported">类型导出设置</param>
    /// <returns><see cref="Type" />[]</returns>
    internal static Type[] GetTypes(this Assembly assembly, bool exported) =>
        exported
            ? assembly.GetExportedTypes()
            : assembly.GetTypes();

    /// <summary>
    ///     获取程序集描述
    /// </summary>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? GetDescription(this Assembly assembly)
    {
        var descriptionAttribute =
            Attribute.GetCustomAttribute(assembly,
                typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute;

        return descriptionAttribute?.Description;
    }

    /// <summary>
    ///     获取程序集版本
    /// </summary>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static Version? GetVersion(this Assembly assembly) => assembly.GetName().Version;

    /// <summary>
    ///     获取程序集名称和版本
    /// </summary>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    /// <param name="separator">分隔符</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string GetNameVersion(this Assembly assembly, string separator = "/") =>
        $"{assembly.GetName().Name}{separator}{assembly.GetVersion()}";

    /// <summary>
    ///     将程序集转换成指定类型返回
    /// </summary>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    internal static TResult ConvertTo<TResult>(this Assembly assembly, Func<Assembly, TResult> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        return configure.Invoke(assembly);
    }
}