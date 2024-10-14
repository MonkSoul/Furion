﻿// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

namespace Furion.Extensions;

/// <summary>
///     委托拓展类
/// </summary>
internal static class DelegateExtensions
{
    /// <summary>
    ///     尝试执行异步委托
    /// </summary>
    /// <param name="func">异步委托</param>
    /// <param name="parameter1">参数 1</param>
    /// <param name="parameter2">参数 2</param>
    /// <typeparam name="T1">参数类型</typeparam>
    /// <typeparam name="T2">参数类型</typeparam>
    internal static async Task TryInvokeAsync<T1, T2>(this Func<T1, T2, Task>? func, T1 parameter1, T2 parameter2)
    {
        // 空检查
        if (func is null)
        {
            return;
        }

        try
        {
            await func(parameter1, parameter2);
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     尝试执行异步委托
    /// </summary>
    /// <param name="func">异步委托</param>
    /// <param name="parameter">参数</param>
    /// <typeparam name="T">参数类型</typeparam>
    internal static async Task TryInvokeAsync<T>(this Func<T, Task>? func, T parameter)
    {
        // 空检查
        if (func is null)
        {
            return;
        }

        try
        {
            await func(parameter);
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     尝试执行异步委托
    /// </summary>
    /// <param name="func">异步委托</param>
    internal static async Task TryInvokeAsync(this Func<Task>? func)
    {
        // 空检查
        if (func is null)
        {
            return;
        }

        try
        {
            await func();
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     尝试执行同步委托
    /// </summary>
    /// <param name="action">同步委托</param>
    /// <param name="parameter1">参数 1</param>
    /// <param name="parameter2">参数 2</param>
    /// <typeparam name="T1">参数类型</typeparam>
    /// <typeparam name="T2">参数类型</typeparam>
    internal static void TryInvoke<T1, T2>(this Action<T1, T2>? action, T1 parameter1, T2 parameter2)
    {
        // 空检查
        if (action is null)
        {
            return;
        }

        try
        {
            action(parameter1, parameter2);
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     尝试执行同步委托
    /// </summary>
    /// <param name="action">同步委托</param>
    /// <param name="parameter">参数</param>
    /// <typeparam name="T">参数类型</typeparam>
    internal static void TryInvoke<T>(this Action<T>? action, T parameter)
    {
        // 空检查
        if (action is null)
        {
            return;
        }

        try
        {
            action(parameter);
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     尝试执行同步委托
    /// </summary>
    /// <param name="action">同步委托</param>
    internal static void TryInvoke(this Action? action)
    {
        // 空检查
        if (action is null)
        {
            return;
        }

        try
        {
            action();
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }
}