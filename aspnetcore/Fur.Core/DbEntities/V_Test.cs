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
    public class V_Test : DbNoKeyEntityOfT<FurDbContextIdentifier>, IDbQueryFilterOfT<V_Test, FurDbContextIdentifier>
    {
        public V_Test() : base("V_Test")
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int TenantId { get; set; }

        public IEnumerable<Expression<Func<V_Test, bool>>> HasQueryFilter(DbContext dbContext)
        {
            var tenantId = dbContext.GetTenantId();
            if (!tenantId.HasValue) return default;

            return new List<Expression<Func<V_Test, bool>>>
            {
               entity=>entity.TenantId==tenantId
            };
        }
    }
}