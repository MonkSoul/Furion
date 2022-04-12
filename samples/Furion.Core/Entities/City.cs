using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Furion.Core;

/// <summary>
/// 城市
/// </summary>
public class City : Entity, IEntityTypeBuilder<City>, IEntitySeedData<City>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public City()
    {
        CreatedTime = DateTimeOffset.Now;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 上级Id
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// 上级
    /// </summary>
    public City Parent { get; set; }

    /// <summary>
    /// 子集
    /// </summary>
    public ICollection<City> Childrens { get; set; }

    /// <summary>
    /// 配置实体关系
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<City> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder
            .HasMany(x => x.Childrens)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行
    }

    /// <summary>
    /// 种子数据
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    public IEnumerable<City> HasData(DbContext dbContext, Type dbContextLocator)
    {
        return new List<City>
        {
            new City { Id=1,CreatedTime =DateTimeOffset.Parse("2020-08-20 15:30:20"),Name="中国" },
            new City { Id=2,CreatedTime =DateTimeOffset.Parse("2020-08-20 15:30:20"),Name="广东省",ParentId=1 },
            new City { Id=3,CreatedTime =DateTimeOffset.Parse("2020-08-20 15:30:20"),Name="中山市",ParentId=2 },
            new City { Id=4,CreatedTime =DateTimeOffset.Parse("2020-08-20 15:30:20"),Name="珠海市",ParentId=2 },
            new City { Id=5,CreatedTime =DateTimeOffset.Parse("2020-08-20 15:30:20"),Name="浙江省",ParentId=1 },
        };
    }
}
