using Fur.ApplicationBase;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fur.Core.DbEntities
{
    [DbTable("Tests", typeof(FurDbContextIdentifier))]
    public class Test : DbEntity, IDbQueryFilterOfT<Test, FurDbContextIdentifier>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public IEnumerable<Expression<Func<Test, bool>>> HasQueryFilter(DbContext dbContext)
        {
            if (!AppGlobal.SupportedMultipleTenant) return default;

            var tenantId = dbContext.GetTenantId();
            return new List<Expression<Func<Test, bool>>>
            {
               entity=>entity.TenantId==tenantId
            };
        }
    }
}