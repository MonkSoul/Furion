// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 基于数据库架构的多租户模式
/// </summary>
public interface IMultiTenantOnSchema : IPrivateMultiTenant
{
    /// <summary>
    /// 获取数据库架构名称
    /// </summary>
    /// <returns></returns>
    string GetSchemaName();
}