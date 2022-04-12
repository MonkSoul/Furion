using Furion.DependencyInjection;

namespace Furion.Core;

/// <summary>
/// 权限常量
/// </summary>
[SuppressSniffer]
public static class SecurityConst
{
    public const string ViewRoles = "ViewRoles";
    public const string ViewSecuries = "ViewSecuries";
    public const string GetRoles = "GetRoles";
    public const string InsertRole = "InsertRole";
    public const string GiveUserRole = "GiveUserRole";
    public const string GiveRoleSecurity = "GiveRoleSecurity";
}
