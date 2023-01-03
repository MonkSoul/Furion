// MIT License
//
// Copyright (c) 2020-2023 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求静态类
/// </summary>
[SuppressSniffer]
public static class Http
{
    /// <summary>
    /// 获取远程请求代理
    /// </summary>
    /// <typeparam name="THttpDispatchProxy">远程请求代理对象</typeparam>
    /// <returns><see cref="GetHttpProxy{THttpDispatchProxy}"/></returns>
    public static THttpDispatchProxy GetHttpProxy<THttpDispatchProxy>()
        where THttpDispatchProxy : class, IHttpDispatchProxy
    {
        return App.GetService<THttpDispatchProxy>(App.RootServices);
    }
}