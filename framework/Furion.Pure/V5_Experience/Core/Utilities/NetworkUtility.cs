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

using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace Furion.Utilities;

/// <summary>
///     提供网络相关的实用方法
/// </summary>
public static class NetworkUtility
{
    // 定义一个私有的静态锁对象用于同步访问
    internal static readonly object PortLock = new();

    /// <summary>
    ///     查找一个可用的 TCP 端口
    /// </summary>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    public static int FindAvailableTcpPort()
    {
        // 定义端口可用范围
        const int fromPort = 10000;
        const int toPort = 65535;

        do
        {
            // 使用锁来确保线程安全地生成和检查端口
            lock (PortLock)
            {
                var randomPort = RandomNumberGenerator.GetInt32(fromPort, toPort + 1);

                // 检查端口是否已经在使用
                if (!IsPortInUse(randomPort))
                {
                    // 如果端口空闲，直接返回
                    return randomPort;
                }
            }

            // 等待一小段时间以避免忙等
            Thread.Sleep(10);
        } while (true);
    }

    /// <summary>
    ///     检查指定端口是否正在使用
    /// </summary>
    /// <remarks>如果端口正在使用则返回 <c>true</c>，否则返回 <c>false</c>。</remarks>
    /// <param name="port">要检查的端口号。</param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool IsPortInUse(int port) =>
        IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any(p => p.Port == port);
}