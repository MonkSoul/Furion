using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Fur.DatabaseAccessor.Repositories.Interceptors
{
    public class FurDbEntityInterceptor : IDbEntityInterceptor
    {
        public void Inserting(EntityEntry entityEntry)
        {
            var property = entityEntry.GetProperty(nameof(DbEntity.CreatedTime));
            if (property != null)
            {
                property.CurrentValue = DateTime.Now;
            }
        }

        public void Inserted(EntityEntry entityEntry)
        {
        }

        public void Updating(EntityEntry entityEntry)
        {
            var property = entityEntry.GetProperty(nameof(DbEntity.UpdatedTime));
            if (property != null)
            {
                property.CurrentValue = DateTime.Now;
                property.IsModified = true;
            }
        }

        public void Updated(EntityEntry entityEntry)
        {
            var property = entityEntry.GetProperty(nameof(DbEntity.CreatedTime));
            if (property != null && property.IsModified)
            {
                property.IsModified = false;
            }
        }
    }
}
