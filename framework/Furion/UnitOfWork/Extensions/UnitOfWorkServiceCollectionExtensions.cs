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

using Furion.DatabaseAccessor;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 工作单元拓展类
/// </summary>
[SuppressSniffer]
public static class UnitOfWorkServiceCollectionExtensions
{
    /// <summary>
    /// 添加工作单元服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <returns>Mvc构建器</returns>
    public static IMvcBuilder AddUnitOfWork<TUnitOfWork>(this IMvcBuilder mvcBuilder)
        where TUnitOfWork : class, IUnitOfWork
    {
        mvcBuilder.Services.AddUnitOfWork<TUnitOfWork>();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加工作单元服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Mvc构建器</returns>
    public static IServiceCollection AddUnitOfWork<TUnitOfWork>(this IServiceCollection services)
        where TUnitOfWork : class, IUnitOfWork
    {
        // 注册全局工作单元过滤器
        services.AddMvcFilter<UnitOfWorkFilter>();

        // 注册工作单元服务
        services.AddTransient<IUnitOfWork, TUnitOfWork>();
        return services;
    }
}