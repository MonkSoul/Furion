using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    [DbContext("DbConnectionString")]
    public class FurDbContext : AppDbContext<FurDbContext>  // 继承 AppDbContext<> 类
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