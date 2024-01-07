// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库执行命令拦截
/// </summary>
internal sealed class SqlCommandProfilerInterceptor : DbCommandInterceptor
{
}