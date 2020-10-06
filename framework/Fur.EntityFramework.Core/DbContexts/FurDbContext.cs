using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    [AppDbContext("Sqlite3ConnectionString")]
    public class FurDbContext : AppDbContext<FurDbContext>
    {
        /// <summary>
        /// 继承父类构造函数
        /// </summary>
        /// <param name="options"></param>
        public FurDbContext(DbContextOptions<FurDbContext> options) : base(options)
        {
        }
    }
}