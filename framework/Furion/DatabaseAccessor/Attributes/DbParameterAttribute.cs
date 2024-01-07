// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Data;

namespace Furion.DatabaseAccessor;

/// <summary>
/// DbParameter 配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Property)]
public sealed class DbParameterAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DbParameterAttribute()
    {
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="direction">参数方向</param>
    public DbParameterAttribute(ParameterDirection direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// 参数输出方向
    /// </summary>
    public ParameterDirection Direction { get; set; }

    /// <summary>
    /// 数据库对应类型
    /// </summary>
    public object DbType { get; set; }

    /// <summary>
    /// 大小
    /// </summary>
    /// <remarks>Nvarchar/varchar类型需指定</remarks>
    public int Size { get; set; }
}