// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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