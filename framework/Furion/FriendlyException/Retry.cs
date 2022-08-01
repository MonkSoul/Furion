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

using System.Diagnostics;

namespace Furion.FriendlyException;

/// <summary>
/// 重试类
/// </summary>
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
    public static void Invoke(Action action, int numRetries, int retryTimeout = 1000, bool finalThrow = true, Type[] exceptionTypes = default)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        InvokeAsync(async () =>
        {
            action();
            await Task.CompletedTask;
        }, numRetries, retryTimeout, finalThrow, exceptionTypes).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 重试有异常的方法，还可以指定特定异常
    /// </summary>
    /// <param name="action"></param>
    /// <param name="numRetries">重试次数</param>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <param name="finalThrow">是否最终抛异常</param>
    /// <param name="exceptionTypes">异常类型,可多个</param>
    public static async Task InvokeAsync(Func<Task> action, int numRetries, int retryTimeout = 1000, bool finalThrow = true, Type[] exceptionTypes = default)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

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
                    if (finalThrow) throw;
                    else return;
                }

                // 如果填写了 exceptionTypes 且异常类型不在 exceptionTypes 之内，则终止重试
                if (exceptionTypes != null && exceptionTypes.Length > 0 && !exceptionTypes.Any(u => u.IsAssignableFrom(ex.GetType())))
                {
                    if (finalThrow) throw;
                    else return;
                }

                if (Debugger.IsAttached)
                {
                    Console.WriteLine($"You can retry {numRetries} more times.");
                }

                // 如果可重试异常数大于 0，则间隔指定时间后继续执行
                if (retryTimeout > 0) await Task.Delay(retryTimeout);
            }
        }
    }
}