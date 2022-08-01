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

using Furion.IPCChannel;

namespace Furion.TaskScheduler;

/// <summary>
/// 定时器监听管道处理程序
/// </summary>
internal sealed class SpareTimeListenerChannelHandler : ChannelHandler<SpareTimerExecuter>
{
    /// <summary>
    /// 触发程序
    /// </summary>
    /// <param name="executer"></param>
    /// <returns></returns>
    public async override Task InvokeAsync(SpareTimerExecuter executer)
    {
        var spareTimeListener = App.GetService<ISpareTimeListener>(App.RootServices);
        if (spareTimeListener == null) return;

        await spareTimeListener.OnListener(executer);
    }
}