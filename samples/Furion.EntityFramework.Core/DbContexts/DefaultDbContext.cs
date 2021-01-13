using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;

namespace Furion.EntityFramework.Core
{
    [AppDbContext("Sqlite3ConnectionString", DbProvider.Sqlite)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        protected override void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
        {
            // 获取当前事件对应上下文
            var dbContext = eventData.Context;

            // 获取所有新增和更新的实体
            var entities = dbContext.ChangeTracker.Entries()
                        .Where(u => u.State == EntityState.Added || u.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Property(nameof(Entity.CreatedTime)).CurrentValue = DateTimeOffset.Now;
                        break;

                    case EntityState.Modified:
                        entity.Property(nameof(Entity.UpdatedTime)).CurrentValue = DateTimeOffset.Now;
                        break;
                }
            }
        }
    }
}