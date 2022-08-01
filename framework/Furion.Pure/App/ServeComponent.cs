// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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