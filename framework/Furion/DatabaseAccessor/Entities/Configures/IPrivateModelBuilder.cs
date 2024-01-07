// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库模型构建器依赖（禁止直接继承）
/// </summary>
/// <remarks>
/// 对应 <see cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)"/>
/// </remarks>
public interface IPrivateModelBuilder
{
}