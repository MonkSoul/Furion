// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Furion.Components;

/// <summary>
/// Serve 组件应用服务组件
/// </summary>
[SuppressSniffer]
public sealed class ServeServiceComponent : IServiceComponent
{
    /// <summary>
    /// 装载服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="componentContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {
        // 配置跨域
        services.AddCorsAccessor();

        // 控制器和规范化结果
        services.AddControllers()
                .AddInjectWithUnifyResult();
    }
}

/// <summary>
/// Serve 组件应用中间件组件
/// </summary>
[SuppressSniffer]
public sealed class ServeApplicationComponent : IApplicationComponent
{
    /// <summary>
    /// 装载中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="componentContext"></param>
    public void Load(IApplicationBuilder app, IWebHostEnvironment env, ComponentContext componentContext)
    {
        // 启用 HTTP 日志记录
        app.UseHttpLogging();

        // 配置错误页
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // 401，403 规范化结果
        app.UseUnifyResultStatusCodes();

        // Https 重定向
        app.UseHttpsRedirection();

        // 配置静态
        app.UseStaticFiles();

        // 配置路由
        app.UseRouting();

        // 配置跨域
        app.UseCorsAccessor();

        // 配置授权
        app.UseAuthentication();
        app.UseAuthorization();

        // 框架基础配置
        app.UseInject(string.Empty);

        // 配置路由
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}