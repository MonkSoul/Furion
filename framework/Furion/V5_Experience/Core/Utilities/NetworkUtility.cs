// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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