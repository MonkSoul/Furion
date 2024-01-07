// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库函数类型
/// </summary>
internal enum DbFunctionType
{
    /// <summary>
    /// 标量函数
    /// </summary>
    Scalar,

    /// <summary>
    /// 表值函数
    /// </summary>
    Table
}