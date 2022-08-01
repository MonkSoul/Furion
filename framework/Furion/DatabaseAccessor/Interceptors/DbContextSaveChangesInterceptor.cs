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

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文提交拦截器
/// </summary>
[SuppressSniffer]
public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// 拦截保存数据库之前
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavingChangesEventInner(eventData, result);

        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// 拦截保存数据库之前
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavingChangesEventInner(eventData, result);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 拦截保存数据库成功
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavedChangesEventInner(eventData, result);

        return base.SavedChanges(eventData, result);
    }

    /// <summary>
    /// 拦截保存数据库成功
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavedChangesEventInner(eventData, result);

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 拦截保存数据库失败
    /// </summary>
    /// <param name="eventData"></param>
    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SaveChangesFailedEventInner(eventData);

        base.SaveChangesFailed(eventData);
    }

    /// <summary>
    /// 拦截保存数据库失败
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SaveChangesFailedEventInner(eventData);

        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}