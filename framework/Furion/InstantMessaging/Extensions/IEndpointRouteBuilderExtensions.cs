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
using Furion.Extensions;
using Furion.InstantMessaging;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 终点路由构建器拓展
/// </summary>
[SuppressSniffer]
public static class IEndpointRouteBuilderExtensions
{
    /// <summary>
    /// 扫描配置所有集线器
    /// </summary>
    /// <param name="endpoints"></param>
    public static void MapHubs(this IEndpointRouteBuilder endpoints)
    {
        // 扫描所有集线器类型并且贴有 [MapHub] 特性且继承 Hub 或 Hub<>
        var hubs = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
            && u.IsDefined(typeof(MapHubAttribute), true)
            && (typeof(Hub).IsAssignableFrom(u) || u.HasImplementedRawGeneric(typeof(Hub<>))));

        if (!hubs.Any()) return;

        // 反射获取 MapHub 拓展方法
        var mapHubMethod = typeof(HubEndpointRouteBuilderExtensions).GetMethods().Where(u => u.Name == "MapHub" && u.IsGenericMethod && u.GetParameters().Length == 3).FirstOrDefault();
        if (mapHubMethod == null) return;

        // 遍历所有集线器并注册
        foreach (var hub in hubs)
        {
            // 解析集线器特性
            var mapHubAttribute = hub.GetCustomAttribute<MapHubAttribute>(true);

            // 创建连接分发器委托
            Action<HttpConnectionDispatcherOptions> configureOptions = options =>
            {
                // 执行连接分发器选项配置
                hub.GetMethod("HttpConnectionDispatcherOptionsSettings", BindingFlags.Public | BindingFlags.Static)
                ?.Invoke(null, new object[] { options });
            };

            // 注册集线器
            var hubEndpointConventionBuilder = mapHubMethod.MakeGenericMethod(hub).Invoke(null, new object[] { endpoints, mapHubAttribute.Pattern, configureOptions }) as HubEndpointConventionBuilder;

            // 执行终点转换器配置
            hub.GetMethod("HubEndpointConventionBuilderSettings", BindingFlags.Public | BindingFlags.Static)
                ?.Invoke(null, new object[] { hubEndpointConventionBuilder });
        }
    }
}