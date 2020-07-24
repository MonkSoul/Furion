using Autofac;
using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fur.Core.DbEntities
{
    public class V_Test : DbNoKeyEntity, IDbQueryFilterOfT<V_Test>
    {
        public V_Test() : base("V_Test")
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int TenantId { get; set; }

        public Dictionary<Expression<Func<V_Test, bool>>, IEnumerable<Type>> HasQueryFilter(DbContext dbContext, ILifetimeScope lifetimeScope)
        {
            var tenantProvider = lifetimeScope.Resolve<IMultiTenantProvider>();
            return new Dictionary<Expression<Func<V_Test, bool>>, IEnumerable<Type>>
            {
                {
                    b => EF.Property<int>(b, nameof(DbEntityBase.TenantId)) == tenantProvider.GetTenantId(),
                    new List<Type>
                    {
                        typeof(FurDbContextIdentifier)
                    }
                }
            };
        }
    }
}