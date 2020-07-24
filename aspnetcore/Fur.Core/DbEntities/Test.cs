using Autofac;
using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Fur.Core.DbEntities
{
    [Table("Tests")]
    public class Test : DbEntityBase, IDbQueryFilterOfT<Test, FurDbContextIdentifier>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public IEnumerable<Expression<Func<Test, bool>>> HasQueryFilter(ILifetimeScope lifetimeScope)
        {
            if (!lifetimeScope.IsRegistered<IMultiTenantProvider>()) return default;

            var tenantProvider = lifetimeScope.Resolve<IMultiTenantProvider>();
            return new List<Expression<Func<Test, bool>>>
            {
                b => EF.Property<int>(b, nameof(DbEntityBase.TenantId)) == tenantProvider.GetTenantId()
            };
        }
    }
}