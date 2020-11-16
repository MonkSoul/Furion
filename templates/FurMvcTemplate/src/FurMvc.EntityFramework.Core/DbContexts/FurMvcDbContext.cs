using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurMvc.EntityFramework.Core
{
    [AppDbContext("FurMvc")]
    public class FurMvcDbContext : AppDbContext<FurMvcDbContext>
    {
        public FurMvcDbContext(DbContextOptions<FurMvcDbContext> options) : base(options)
        {
        }
    }
}