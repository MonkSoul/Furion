// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文提交拦截器
    /// </summary>
    public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
    {
    }
}