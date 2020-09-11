using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Fur.Core
{
    public class TestConfigure : IEntityTypeBuilder<Test>, IEntitySeedData<Test>
    {
        public void Configure(EntityTypeBuilder<Test> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }

        public IEnumerable<Test> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<Test>
            {
                new Test
                {
                    Id=1,
                    Name="百小僧"
                }
            };
        }
    }
}