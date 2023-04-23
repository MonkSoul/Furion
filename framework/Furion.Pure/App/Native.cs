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
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;

namespace System;

/// <summary>
/// 用于原生应用（WinForm/WPF）创建窗口
/// </summary>
[SuppressSniffer]
public static class Native
{
    /// <summary>
    /// 创建原生应用（WinForm/WPF）窗口
    /// </summary>
    /// <typeparam name="TWindow"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static TWindow CreateInstance<TWindow>(params object[] parameters)
        where TWindow : class
    {
        return CreateInstance(typeof(TWindow), parameters) as TWindow;
    }

    /// <summary>
    /// 创建原生应用（WinForm/WPF）组件窗口
    /// </summary>
    /// <param name="windowType"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static object CreateInstance(Type windowType, params object[] parameters)
    {
        // 获取构造函数
        var constructors = windowType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        // 如果构造函数为空，则直接创建返回
        if (constructors.Length == 0) return Activator.CreateInstance(windowType);

        // 检查是否包含多个公开构造函数
        if (constructors.Length > 1) throw new InvalidOperationException($"Multiple constructors accepting all given argument types have been found in type '{windowType.Namespace}.{windowType.Name}'. There should only be one applicable constructor.");

        // 获取唯一构造函数参数
        var parameterInfos = constructors[0].GetParameters();

        // 准备构造函数参数
        var ctorParameters = new List<object>();

        // 创建服务作用域
        var serviceScope = App.RootServices.CreateScope();

        // 遍历构造函数参数
        for (var i = 0; i < parameterInfos.Length; i++)
        {
            var parameterInfo = parameterInfos[i];

            var serviceType = parameterInfo.ParameterType;
            object serviceInstance;

            // 获取服务注册生命周期
            var serviceLifetime = App.GetServiceLifetime(serviceType);

            // 如果构造函数不是服务类型，则直接跳出
            if (serviceLifetime == null) break;

            // 如果是单例，直接从根服务解析
            if (serviceLifetime == ServiceLifetime.Singleton)
            {
                serviceInstance = App.RootServices.GetService(serviceType);
            }
            // 否则通过作用域解析
            else
            {
                serviceInstance = serviceScope.ServiceProvider.GetService(serviceType);
            }

            ctorParameters.Add(serviceInstance);
        }

        // 创建窗口实例
        var windowInstance = Activator.CreateInstance(windowType, ctorParameters.Concat(parameters).ToArray());

        // 获取 Owner 属性并绑定关闭事件
        var ownerProperty = windowType.GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public);
        if (ownerProperty != null
            && (ownerProperty.PropertyType.FullName.StartsWith("System.Windows.Forms.Form")
                || ownerProperty.PropertyType.FullName.StartsWith("System.Windows.Window")))
        {
            var propertyType = ownerProperty.PropertyType;

            // 监听窗口关闭事件
            void closedHandler(object s, EventArgs e)
            {
                // 释放作用域
                serviceScope.Dispose();
            }

            var closedEventInfo = windowType.GetEvent("Closed", BindingFlags.Instance | BindingFlags.Public);
            closedEventInfo.AddEventHandler(windowInstance, new EventHandler((Action<object, EventArgs>)closedHandler));
        }

        return windowInstance;
    }

    /// <summary>
    /// 获取一个空闲端口
    /// </summary>
    /// <returns></returns>
    public static int GetIdlePort()
    {
        var fromPort = 10000;
        var toPort = 65535;
        var randomPort = RandomNumberGenerator.GetInt32(fromPort, toPort);

        while (IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any(p => p.Port == randomPort))
        {
            randomPort = RandomNumberGenerator.GetInt32(fromPort, toPort);
        }

        return randomPort;
    }
}