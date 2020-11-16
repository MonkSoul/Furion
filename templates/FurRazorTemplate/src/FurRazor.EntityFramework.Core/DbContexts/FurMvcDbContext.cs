using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurRazor.EntityFramework.Core
{
    [AppDbContext("FurRazor")]
    public class FurRazorDbContext : AppDbContext<FurRazorDbContext>
    {
        public FurRazorDbContext(DbContextOptions<FurRazorDbContext> options) : base(options)
        {
        }
    }
}