using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Furion.TestProject;

/// <summary>
/// 全局对象 App 测试
/// </summary>
public class GlobalAppTests : IDynamicApiController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHost _host;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GlobalAppTests(IServiceProvider serviceProvider
        , IHost host
        , IHttpContextAccessor httpContextAccessor)
    {
        _serviceProvider = serviceProvider;
        _host = host;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 测试各个服务提供器是否一致
    /// </summary>
    /// <param name="methodServiceProvider"></param>
    /// <param name="methodHttpContextAccessor"></param>
    /// <returns></returns>
    public bool TestServiceProvider([FromServices] IServiceProvider methodServiceProvider, [FromServices] IHttpContextAccessor methodHttpContextAccessor)
    {
        var isSameHttpContextAccessor = (App.RootServices.GetService<IHttpContextAccessor>() == _serviceProvider.GetService<IHttpContextAccessor>())
            && (_httpContextAccessor == methodHttpContextAccessor)
            && (_httpContextAccessor == _serviceProvider.GetService<IHttpContextAccessor>());
        if (!isSameHttpContextAccessor) throw new Exception("HttpContextAccessor 服务不相等");

        var isSameServiceProvider = (_serviceProvider == _httpContextAccessor.HttpContext.RequestServices)
            && (_serviceProvider == methodServiceProvider)
            && (_serviceProvider == methodHttpContextAccessor.HttpContext.RequestServices)
            && (_serviceProvider == App.RootServices.GetService<IHttpContextAccessor>().HttpContext.RequestServices);

        if (!isSameServiceProvider) throw new Exception("当前请求 ServiceProvider 服务不相等");

        var noSameServiceProvider = App.RootServices != _serviceProvider.CreateScope().ServiceProvider;

        return noSameServiceProvider;
    }
}