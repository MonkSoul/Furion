using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furion.Core;

/// <summary>
/// 用户表
/// </summary>
public class User : EntityBase, IEntitySeedData<User>, IEntityTypeBuilder<User>
{
    /// <summary>
    /// 账号
    /// </summary>
    [StringLength(32)]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [StringLength(32)]
    public string Password { get; set; }

    /// <summary>
    /// 多对多
    /// </summary>
    public ICollection<Role> Roles { get; set; }

    /// <summary>
    /// 多对多中间表
    /// </summary>
    public List<UserRole> UserRoles { get; set; }

    /// <summary>
    /// 配置多对多关系
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasMany(p => p.Roles)
             .WithMany(p => p.Users)
             .UsingEntity<UserRole>(
               u => u.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId)
             , u => u.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId)
             , u =>
             {
                 u.HasKey(c => new { c.UserId, c.RoleId });
                 u.HasData(new { UserId = 1, RoleId = 1 });
             });
    }

    /// <summary>
    /// 种子数据
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    public IEnumerable<User> HasData(DbContext dbContext, Type dbContextLocator)
    {
        return new[]
        {
            new User
            {
                Id=1,Account="admin",Password="admin"
            },
            new User
            {
                Id=2,Account="Furion",Password="dotnetchina"
            }
        };
    }
}
