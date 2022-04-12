using Furion.DatabaseAccessor;

namespace Furion.Core;

/// <summary>
/// 用户和角色关系表
/// </summary>
public class UserRole : IEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public int UserId { get; set; }

    public User User { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public int RoleId { get; set; }

    public Role Role { get; set; }
}
