// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 对象映射拓展类
/// </summary>
public static class ObjectMapperServiceCollectionExtensions
{
    /// <summary>
    /// 添加对象映射
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblies">扫描的程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddObjectMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        // 获取全局映射配置
        var config = TypeAdapterConfig.GlobalSettings;

        // 扫描所有继承  IRegister 接口的对象映射配置
        if (assemblies != null && assemblies.Length > 0) config.Scan(assemblies);

        // 配置默认全局映射（支持覆盖）
        config.Default
              .NameMatchingStrategy(NameMatchingStrategy.Flexible)
              .PreserveReference(true);

        // 配置默认全局映射（忽略大小写敏感）
        config.Default
              .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
              .PreserveReference(true);

        // 配置支持依赖注入
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}