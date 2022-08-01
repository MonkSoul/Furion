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

using Furion;
using Furion.SpecificationDocument;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 规范化接口服务拓展类
/// </summary>
[SuppressSniffer]
public static class SpecificationDocumentServiceCollectionExtensions
{
    /// <summary>
    /// 添加规范化文档服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc 构建器</param>
    /// <param name="configure">自定义配置</param>
    /// <returns>服务集合</returns>
    public static IMvcBuilder AddSpecificationDocuments(this IMvcBuilder mvcBuilder, Action<SpecificationDocumentServiceOptions> configure = default)
    {
        mvcBuilder.Services.AddSpecificationDocuments(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加规范化文档服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configure">自定义配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services, Action<SpecificationDocumentServiceOptions> configure = default)
    {
        // 判断是否启用规范化文档
        if (App.Settings.InjectSpecificationDocument != true) return services;

        // 添加配置
        services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

        // 载入服务配置选项
        var configureOptions = new SpecificationDocumentServiceOptions();
        configure?.Invoke(configureOptions);

        // 添加Swagger生成器服务
        services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, configureOptions?.SwaggerGenConfigure));

        // 添加 MiniProfiler 服务
        AddMiniProfiler(services);

        return services;
    }

    /// <summary>
    /// 添加 MiniProfiler 配置
    /// </summary>
    /// <param name="services"></param>
    private static void AddMiniProfiler(IServiceCollection services)
    {
        // 注册MiniProfiler 组件
        if (App.Settings.InjectMiniProfiler != true) return;

        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/index-mini-profiler";
            options.EnableMvcFilterProfiling = false;
            options.EnableMvcViewProfiling = false;
        });
    }
}