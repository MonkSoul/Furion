// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求静态类
/// </summary>
[SuppressSniffer]
public static class Http
{
    /// <summary>
    /// 获取远程请求代理
    /// </summary>
    /// <typeparam name="THttpDispatchProxy">远程请求代理对象</typeparam>
    /// <returns><see cref="GetHttpProxy{THttpDispatchProxy}"/></returns>
    public static THttpDispatchProxy GetHttpProxy<THttpDispatchProxy>()
        where THttpDispatchProxy : class, IHttpDispatchProxy
    {
        return App.GetService<THttpDispatchProxy>(App.RootServices);
    }
}