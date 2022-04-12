using Furion.DatabaseAccessor;

namespace Furion.Core;

/// <summary>
/// 角色和权限关系表
/// </summary>
public class RoleSecurity : IEntity
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public int RoleId { get; set; }

    public Role Role { get; set; }

    /// <summary>
    /// 权限Id
    /// </summary>
    public int SecurityId { get; set; }

    public Security Security { get; set; }
}
