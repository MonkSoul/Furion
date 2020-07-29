# 数据库上下文

在 `EF Core` 项目中，数据库的操作都是通过 `DbContext` 数据库上下文处理的。

---

## 关于数据库上下文

简单来说，`DbContext` 是实体类和数据库之间的桥梁，`DbContext` 主要负责与数据交互。

::: tip 主要作用

- 包含所有的实体映射到数据库表的实体集 (`DbSet<TEntity>`)

- 将 `LINQ-to-Entities` 查询转换为 `SQL查询` 并将其发送到数据库

- 跟踪每个实体从数据库中查询出来后发生的修改变化

- 基于实体状态执行插入、更新和删除操作到数据库中

:::

## 创建 DbContext 上下文

在 `EF Core` 中，所有自定义的数据库上下文都需要继承 `DbContext`，位于 `Microsoft.EntityFrameworkCore` 命名空间下，如：

```cs {3-4,7,12,18-19}
public class FurBookContext : DbContext
{
    public FurSqlServerContext(DbContextOptions<BloggingContext> options)
        : base(options)
    { }

    public DbSet<Book> Books { get; set; }

    // 配置数据库提供器及连接字符串等信息
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Server=localhost;Database=Fur;User=sa;Password=000000;MultipleActiveResultSets=True;");
    }

    // 配置实体信息
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);
    }
}
```

::: warning 特别注意
`EF Core` 默认提供的 `DbContext` 在实现某些场景下实现极其复杂，如：**多租户，主从库/读写分离，多数据库上下文**。

所以，**Fur 框架推荐使用 `FurDbContextOfT<TDbContext, TDbContextLocator>`**
:::

## 关于 FurDbContextOfT<TDbContext, TDbContextLocator>

`FurDbContextOfT<TDbContext, TDbContextLocator>` 是 Fur 框架基于 `DbContext` 抽象出的子类并拥有前者全部功能的同时还支持**多租户，主从库/读写分离，多数据库上下文**等复杂操作。

::: warning 读者说明
为了便于属性，`FurDbContextOfT<TDbContext, TDbContextLocator>` 在后续章节中简称 **`FurDbContext`**。
:::

::: details 查看两者的区别

- `FurDbContext` 是 继承 `DbContext` 的抽象子类，本身无实现

- `FurDbContext` 初始化需提供 [数据库上下文定位器](/handbook/database-accessor/dbcontext-locator.html)，这是和 `DbContext` 最大的区别，关于 [数据库上下文定位器](/handbook/database-accessor/dbcontext-locator.html) 将在下一章节说明

- `FurDbContext` 支持自动配置表、视图、函数、存储过程配置

- `FurDbContext` 支持种子数据、查询拦截器、全局拦截器等配置

- `FurDbContext` 支持更多未来特性

:::

所以，在 Fur 框架中，**推荐使用 `FurDbContext`创建数据库上下文，而不是 `DbContext`**。

### 创建 FurDbContext 上下文

创建 FurDbContext 上下文需要继承 `FurDbContextOfT<TDbContext, TDbContextLocator>` 并提供 [数据库上下文定位器](/handbook/database-accessor/dbcontext-locator.html)。

::: tip 主要作用
在 Fur 框架中，已经提供了默认 [数据库上下文定位器](/handbook/database-accessor/dbcontext-locator.html)：**`FurDbContextLocator`**
:::

代码如下：

```cs {7,9-10}
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Locators;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurSqlServerDbContext : FurDbContextOfT<FurSqlServerDbContext, FurDbContextLocator>
    {
        // 无需配置 DbSet<TEntity>，无需重写 OnConfiguring，OnModelCreating
        // 框架会自动在启动时配置好这一切！！！😂

        public FurSqlServerDbContext(DbContextOptions<FurSqlServerDbContext> options)
            : base(options)
        {
        }
    }
}
```

::: warning 存放位置
数据库上下文建议放在 `Fur.Entityframework.Core` 层的 `DbContexts` 目录下。
:::

::: tip 唯一代价
`FurDbContext` 相对 `DbContext` 唯一的代价是需要提供 [数据库上下文定位器](/handbook/database-accessor/dbcontext-locator.html)，但是后续带来的收益确是无量的。
:::

## 初始化数据库上下文

我们创建好 数据库上下文 类后，需要在 `Fur.Web.Host` 的 `Startup.cs` 类的 `void ConfigureServices(IServiceCollection services)` 方法中初始化。

但是，Fur 框架提供了更加便捷的方式，如：

### 配置连接字符串

```json {10-13}
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  // 连接字符串
  "ConnectionStrings": {
    "FurConnectionString": "Server=localhost;Database=Fur;User=sa;Password=000000;MultipleActiveResultSets=True;"
  },

  ...
}
```

### 添加到数据库上下文池中

打开 `Fur.EntityFramework.Core.DbContextServiceExtensions.cs` 文件，并写入如下代码：

```cs {16-18}
using Fur.DatabaseAccessor.Extensions.Services;
using Fur.DatabaseAccessor.Filters;
using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.EntityFramework.Core.Extensions
{
    public static class DbContextServiceExtensions
    {
        public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            // 添加到数据库连接池中，也可以不采用连接池，如：services.AddFurSqlServerDbContext<FurSqlServerDbContext>(...);
            services.AddFurSqlServerDbContextPool<FurSqlServerDbContext>(
                configuration.GetConnectionString("FurConnectionString"), env); // 读取连接字符串
            
            return services;
        }
    }
}
```

-----

😀😁😂🤣😃😄😍😎

::: details 了解更多

想了解更多 `DbContext` 知识可查阅 [EF Core - 配置 DbContext](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/configuring-dbcontext) 章节。

:::