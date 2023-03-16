// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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