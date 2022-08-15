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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 动态模型缓存工厂
/// </summary>
/// <remarks>主要用来实现数据库分表分库</remarks>
[SuppressSniffer]
public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
{
    /// <summary>
    /// 动态模型缓存Key
    /// </summary>
    private static int cacheKey;

    /// <summary>
    /// 重写构建模型
    /// </summary>
    /// <remarks>动态切换表之后需要调用该方法</remarks>
    public static void RebuildModels()
    {
        Interlocked.Increment(ref cacheKey);
    }
#if NET5_0
    /// <summary>
    /// 更新模型缓存
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public object Create(DbContext context)
    {
        return (context.GetType(), cacheKey);
    }
#else
    /// <summary>
    /// 更新模型缓存
    /// </summary>
    /// <param name="context"></param>
    /// <param name="designTime"></param>
    /// <returns></returns>
    public object Create(DbContext context, bool designTime)
    {
        return (context.GetType(), cacheKey);
    }
#endif
}