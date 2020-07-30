using Fur.DatabaseAccessor.Models.SeedDatas;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Fur.Core.DbEntities
{
    public class TenantDbSeedData : IDbSeedData<Tenant>
    {
        public IEnumerable<Tenant> HasData(DbContext dbContext)
        {
            return new List<Tenant>()
            {
                new Tenant
                {
                    TenantId = Guid.NewGuid(),
                    Name = "默认租户",
                    Host = "localhost:44307",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    IsDeleted = false,
                    Schema = "fur",
                    ConnectionString = "Server=localhost;Database=Fur;User=sa;Password=000000;MultipleActiveResultSets=True;"
                },
                new Tenant
                {
                    TenantId = Guid.NewGuid(),
                    Name = "其他租户",
                    Host = "localhost:41529",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    IsDeleted = false,
                    Schema = "other",
                    ConnectionString = "Server=localhost;Database=Other;User=sa;Password=000000;MultipleActiveResultSets=True;"
                }
            };
        }
    }
}