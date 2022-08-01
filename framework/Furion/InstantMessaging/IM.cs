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

using Microsoft.AspNetCore.SignalR;

namespace Furion.InstantMessaging;

/// <summary>
/// 即时通信静态类
/// </summary>
public static class IM
{
    /// <summary>
    /// 获取集线器实例
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IHubContext<THub> GetHub<THub>(IServiceProvider serviceProvider = default)
        where THub : Hub
    {
        return App.GetService<IHubContext<THub>>(serviceProvider);
    }

    /// <summary>
    /// 获取强类型集线器实例
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    /// <typeparam name="TStronglyTyped"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IHubContext<THub, TStronglyTyped> GetHub<THub, TStronglyTyped>(IServiceProvider serviceProvider = default)
        where THub : Hub<TStronglyTyped>
        where TStronglyTyped : class
    {
        return App.GetService<IHubContext<THub, TStronglyTyped>>(serviceProvider);
    }
}