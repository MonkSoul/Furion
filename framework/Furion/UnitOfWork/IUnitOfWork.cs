// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 工作单元依赖接口
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// 工作单元未标记处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="isManual"></param>
    void OnUnmark(ActionExecutedContext resultContext, bool isManual);

    /// <summary>
    /// 开启工作单元处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfwork"></param>
    /// <param name="isManual"></param>
    void BeginTransaction(ActionExecutingContext context, UnitOfWorkAttribute unitOfwork, bool isManual);

    /// <summary>
    /// 提交工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfwork"></param>
    /// <param name="isManual"></param>
    void CommitTransaction(ActionExecutedContext resultContext, UnitOfWorkAttribute unitOfwork, bool isManual);

    /// <summary>
    /// 回滚工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfwork"></param>
    /// <param name="isManual"></param>
    void RollbackTransaction(ActionExecutedContext resultContext, UnitOfWorkAttribute unitOfwork, bool isManual);

    /// <summary>
    /// 执行完毕（无论成功失败）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resultContext"></param>
    void OnCompleted(ActionExecutingContext context, ActionExecutedContext resultContext);
}