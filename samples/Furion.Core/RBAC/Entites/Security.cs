using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Furion.Core;

/// <summary>
/// 权限表
/// </summary>
public class Security : EntityBase, IEntitySeedData<Security>
{
    /// <summary>
    /// 权限唯一名（每一个接口）
    /// </summary>
    public string UniqueName { get; set; }

    /// <summary>
    /// 多对多
    /// </summary>
    public ICollection<Role> Roles { get; set; }

    /// <summary>
    /// 多对多中间表
    /// </summary>
    public List<RoleSecurity> RoleSecurities { get; set; }

    /// <summary>
    /// 种子数据
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    public IEnumerable<Security> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var securities = typeof(SecurityConst).GetFields().Select(u => u.GetRawConstantValue().ToString()).ToArray();
        var list = new List<Security>();
        for (var i = 1; i < securities.Length + 1; i++)
        {
            list.Add(new Security { Id = i, UniqueName = securities[i - 1] });
        }

        return list;
    }
}
