# 配置

可持续拓展的应用必须具备可灵活配置的功能。

---

## 关于配置 <Badge text="不推荐" type="error" />

简单来说，配置就是将系统应用可动态调配的选项放在统一地方管理。在 `ASP.NET Core` 应用程序中，配置通常存放在启动项目 `appsetting.json` 文件中。

::: warning 推荐说明
推荐使用 **《[选项](/handbook/configuration-options/options)》** 方式替代当前配置。
:::

## 如何使用

例如，我们需要动态配置应用的名称、版本号及版权信息。

### 配置 `appsetting.json`

```json {2-6}
{
  "AppInfo": {
    "Name": "Fur",
    "Version": "1.0.0",
    "Company": "Baiqian"
  }
}
```

### 读取配置

#### 🥒 通过依赖注入方式读取

```cs {2,11-13}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    public class DefaultController : ControllerBase
    {
        public DefaultController(IConfiguration configuration)
        {
            var name = configuration["AppInfo:Name"];
            var version = configuration["AppInfo:Version"];
            var company = configuration["AppInfo:Company"];
        }
    }
}
```

#### 🥒 通过 `App.Configuration` 读取

```cs {10-12}
using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    public class DefaultController : ControllerBase
    {
        public DefaultController()
        {
            var name = App.Configuration["AppInfo:Name"];
            var version = App.Configuration["AppInfo:Version"];
            var company = App.Configuration["AppInfo:Company"];
        }
    }
}
```

::: tip App.Configuration 源码
```cs
public static IConfiguration Configuration { 
    get => ServiceProvider.GetService<IConfiguration>(); 
}
```

实际上和依赖注入无异，只是提供了更加便捷的方式。
:::

## 节点读取

通过上面的例子我们不难发现，我们读取 `AppInfo` 第一级子节点是通过 `:` 连接键的，如：`AppInfo:Name`，相同的原理，如果我们需要读取二级、三级、四级、N级只需要通过 `:` 连接即可，如：

- 读取二级

```cs
App.Configuration["Root:One:Two"]
```

- 读取三级

```cs
App.Configuration["Root:One:Two:Three"]
```

读取N级以此类推。

---

😀😁😂🤣😃😄😍😎

::: details 了解更多

想了解更多 `配置` 知识可查阅 [ASP.NET Core - 配置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0) 章节。

:::
