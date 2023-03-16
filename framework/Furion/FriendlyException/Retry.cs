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

namespace Furion.FriendlyException;

/// <summary>
/// 重试静态类
/// </summary>
[SuppressSniffer]
public sealed class Retry
{
    /// <summary>
    /// 重试有异常的方法，还可以指定特定异常
    /// </summary>
    /// <param name="action"></param>
    /// <param name="numRetries">重试次数</param>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <param name="finalThrow">是否最终抛异常</param>
    /// <param name="exceptionTypes">异常类型,可多个</param>
    /// <param name="fallbackPolicy">重试失败回调</param>
    /// <param name="retryAction">重试时调用方法</param>
    public static void Invoke(Action action
        , int numRetries
        , int retryTimeout = 1000
        , bool finalThrow = true
        , Type[] exceptionTypes = default
        , Action<Exception> fallbackPolicy = default
        , Action<int, int> retryAction = default)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        InvokeAsync(async () =>
        {
            action();
            await Task.CompletedTask;
        }, numRetries, retryTimeout, finalThrow, exceptionTypes, fallbackPolicy == null ? null
        : async (ex) =>
        {
            fallbackPolicy?.Invoke(ex);
            await Task.CompletedTask;
        }, retryAction).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 重试有异常的方法，还可以指定特定异常
    /// </summary>
    /// <param name="action"></param>
    /// <param name="numRetries">重试次数</param>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <param name="finalThrow">是否最终抛异常</param>
    /// <param name="exceptionTypes">异常类型,可多个</param>
    /// <param name="fallbackPolicy">重试失败回调</param>
    /// <param name="retryAction">重试时调用方法</param>
    /// <returns><see cref="Task"/></returns>
    public static async Task InvokeAsync(Func<Task> action
        , int numRetries
        , int retryTimeout = 1000
        , bool finalThrow = true
        , Type[] exceptionTypes = default
        , Func<Exception, Task> fallbackPolicy = default
        , Action<int, int> retryAction = default)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        // 如果重试次数小于或等于 0，则直接调用
        if (numRetries <= 0)
        {
            await action();
            return;
        }

        // 存储总的重试次数
        var totalNumRetries = numRetries;

        // 不断重试
        while (true)
        {
            try
            {
                await action();
                break;
            }
            catch (Exception ex)
            {
                // 如果可重试次数小于或等于0，则终止重试
                if (--numRetries < 0)
                {
                    if (finalThrow)
                    {
                        if (fallbackPolicy != null) await fallbackPolicy.Invoke(ex);
                        throw;
                    }
                    else return;
                }

                // 如果填写了 exceptionTypes 且异常类型不在 exceptionTypes 之内，则终止重试
                if (exceptionTypes != null && exceptionTypes.Length > 0 && !exceptionTypes.Any(u => u.IsAssignableFrom(ex.GetType())))
                {
                    if (finalThrow)
                    {
                        if (fallbackPolicy != null) await fallbackPolicy.Invoke(ex);
                        throw;
                    }
                    else return;
                }

                // 重试调用委托
                retryAction?.Invoke(totalNumRetries, totalNumRetries - numRetries);

                // 如果可重试异常数大于 0，则间隔指定时间后继续执行
                if (retryTimeout > 0) await Task.Delay(retryTimeout);
            }
        }
    }
}