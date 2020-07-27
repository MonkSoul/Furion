using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Fur.DatabaseAccessor.Repositories.Interceptors
{
    public class FurDbEntityInterceptor : IDbEntityInterceptor
    {
        public void Inserting(EntityEntry entityEntry)
        {
            if (entityEntry.Metadata.FindProperty(nameof(DbEntity.CreatedTime)) != null)
            {
                var createdTimeProperty = entityEntry.Property(nameof(DbEntity.CreatedTime));
                createdTimeProperty.CurrentValue = DateTime.Now;
            }
        }

        public void Inserted(EntityEntry entityEntry)
        {
        }

        public void Updating(EntityEntry entityEntry)
        {
            if (entityEntry.Metadata.FindProperty(nameof(DbEntity.UpdatedTime)) != null)
            {
                var updatedTimeProperty = entityEntry.Property(nameof(DbEntity.UpdatedTime));
                updatedTimeProperty.CurrentValue = DateTime.Now;
                updatedTimeProperty.IsModified = true;
            }
        }

        public void Updated(EntityEntry entityEntry)
        {
            if (entityEntry.Metadata.FindProperty(nameof(DbEntity.CreatedTime)) != null)
            {
                var createdTimeProperty = entityEntry.Property(nameof(DbEntity.CreatedTime));
                createdTimeProperty.IsModified = false;
            }
        }
    }
}
