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

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 规范化文档中间件拓展
/// </summary>
[SuppressSniffer]
public static class SpecificationDocumentApplicationBuilderExtensions
{
    /// <summary>
    /// 添加规范化文档中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routePrefix"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSpecificationDocuments(this IApplicationBuilder app, string routePrefix = default, Action<SpecificationDocumentConfigureOptions> configure = default)
    {
        // 判断是否启用规范化文档
        if (App.Settings.InjectSpecificationDocument != true) return app;

        // 载入服务配置选项
        var configureOptions = new SpecificationDocumentConfigureOptions();
        configure?.Invoke(configureOptions);

        // 配置 Swagger 全局参数
        app.UseSwagger(options => SpecificationDocumentBuilder.Build(options, configureOptions?.SwaggerConfigure));

        // 配置 Swagger UI 参数
        app.UseSwaggerUI(options => SpecificationDocumentBuilder.BuildUI(options, routePrefix, configureOptions?.SwaggerUIConfigure));

        // 启用 MiniProfiler组件
        if (App.Settings.InjectMiniProfiler == true) app.UseMiniProfiler();

        return app;
    }
}