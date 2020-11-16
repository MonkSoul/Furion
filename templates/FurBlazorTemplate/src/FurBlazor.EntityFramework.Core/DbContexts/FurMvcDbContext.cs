using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurBlazor.EntityFramework.Core
{
    [AppDbContext("FurBlazor")]
    public class FurBlazorDbContext : AppDbContext<FurBlazorDbContext>
    {
        public FurBlazorDbContext(DbContextOptions<FurBlazorDbContext> options) : base(options)
        {
        }
    }
}