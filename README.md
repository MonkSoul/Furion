<p></p>
<p></p>

<p align="center">
<img src="./handbook/static/img/furionlogo.png" height="80"/>
</p>

<div align="center">

[![star](https://gitee.com/monksoul/Furion/badge/star.svg?theme=gvp)](https://gitee.com/monksoul/Furion/stargazers) [![fork](https://gitee.com/monksoul/Furion/badge/fork.svg?theme=gvp)](https://gitee.com/monksoul/Furion/members) [![GitHub stars](https://img.shields.io/github/stars/MonkSoul/Furion?logo=github)](https://github.com/MonkSoul/Furion/stargazers) [![GitHub forks](https://img.shields.io/github/forks/MonkSoul/Furion?logo=github)](https://github.com/MonkSoul/Furion/network) [![GitHub license](https://img.shields.io/badge/license-Apache2-yellow)](https://gitee.com/monksoul/Furion/blob/master/LICENSE) [![nuget](https://img.shields.io/nuget/v/Furion.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion)

</div>

<div align="center">

Make .NET development easier, more versatile, and more popular.

</div>

English | [简体中文](./README-zh.md)

## 💐 Preface

> Selfless dedication is not a fantasy. Sometimes, we can do it.

## 🍕 Naming

> The story goes like this:
>
> Since Microsoft announced the news of the `.NET 5` platform, I have been thinking about developing a development framework based on the `.NET 5` platform. I wants to be the first to eat the `.NET 5` crab.
>
> At the beginning, I thought of `Lazier` as the name of the framework, which means `更懒` in Chinese. In line with my "all simple, just to be lazy" development philosophy.
>
> But **Lazier** and **Worse** have similar pronunciation and no characteristics, and the meaning is not very good. I have been agonizing about the name change for several days.
>
> Just once in QQ group accidentally brush to chat record hair **Furion** word, meaning is a prophet, at that moment, recognized it!
>
> `Furion` means "prophet" in Chinese, which is just in line with my original intention of creating the framework. So, `Furion` was born.

## 🍔 About LOGO

`Furion`'s logo design concept is based on [The Famous Kangaroo Theory](https://baike.baidu.com/item/%E8%A2%8B%E9%BC%A0%E7%90%86%E8%AE%BA).

Kangaroo has the characteristics of **long legs, bagging and self running**.

- `Long legs`: It means that `Furion` has steady legs, walks in the forefront of science and technology, goes further and runs faster.
- `Brood bag`: Small bags and big achievements. It is expected that `Furion` can breed more. Net excellent developers and vibrant ecology.
- `Self run`: `Furion` itself should keep learning, making progress, innovating and developing.

<p>
<img src="./handbook/static/img/furionlogo.png" height="120"/>
</p>

## 🍟 Document

- Domestic documents: [https://monksoul.gitee.io/furion/](https://monksoul.gitee.io/furion/)
- Foreign documents: [https://furion.pro](https://furion.pro)

**At present, the document is gradually improving.**

## 🍯 Souce Cases

- **[ExamKing](https://gitee.com/pig0224/ExamKing)**：Online examination system based on `Furion`.
- **[Gardener](https://gitee.com/hgflydream/Gardener)**：Super simple system based on `Furion` and `Blazor`.
- **[Queer](https://gitee.com/songzhidan/queer)**：General system based on `Furion` and `Layui`.
- **[Pear Admin](https://gitee.com/pear-admin/pear-admin-furion)**：General system based on `Furion` and `PearAdmin`.
- **[JoyAdmin](https://gitee.com/a106_admin/joy-admin)**：General system based on `Furion` and `iviewadmin`.
- **[YShop](https://gitee.com/yell-run/yshop)**：Mobile e-commerce project based on `Furion` and `Vue`.

## 🥦 Tutorials

- **Furion Video tutorial: [https://space.bilibili.com/695987967](https://space.bilibili.com/695987967)**
- Furion Samples：[https://gitee.com/monksoul/furion-samples](https://gitee.com/monksoul/furion-samples) **Maybe most of the examples are not common anymore**
- Furion Series of tutorials: [《Learn .NET 5 from Furion》](https://gitee.com/monksoul/Furion/blob/main/tutorials)

## 🌭 Souce Code

- Gitee：[https://gitee.com/monksoul/Furion](https://gitee.com/monksoul/Furion)
- GitHub：[https://github.com/monksoul/Furion](https://github.com/monksoul/Furion)
- Docker：[https://hub.docker.com/r/monksoul/furion](https://hub.docker.com/r/monksoul/furion)
- Nuget：[https://www.nuget.org/packages/Furion](https://www.nuget.org/packages/Furion)

## 🍿 Docker Image

- `Docker Hub` online Image

```shell
docker run --name furion -p 5000:80 monksoul/furion:v1.4.3
```

- `Manually` pack Image.

Open `CMD/Shell/PowerShell` and enter the `Furion` project root directory to package `Furion` image:

```shell
docker build -t furion:v1.4.3 .
```

When it build successful,then run `docker run`：

```shell
docker run --name furion -p 5000:80 furion:v1.4.3
```

## 🥥 Packages

|                                                                  Package Type                                                                   | Name                                       |                                                                                         Version                                                                                         | Description                             |
| :---------------------------------------------------------------------------------------------------------------------------------------------: | ------------------------------------------ | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | --------------------------------------- |
|                   [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion)                   | Furion                                     |                                     [![nuget](https://img.shields.io/nuget/v/Furion.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion)                                     | `Furion` framework package              |
|   [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.Authentication.JwtBearer)   | Furion.Extras.Authentication.JwtBearer     |     [![nuget](https://img.shields.io/nuget/v/Furion.Extras.Authentication.JwtBearer.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.Authentication.JwtBearer)     | `Furion Jwt` expansion package          |
| [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.DependencyModel.CodeAnalysis) | Furion.Extras.DependencyModel.CodeAnalysis | [![nuget](https://img.shields.io/nuget/v/Furion.Extras.DependencyModel.CodeAnalysis.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.DependencyModel.CodeAnalysis) | `Furion CodeAnalysis` expansion package |
|       [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.Web.HttpContext)        | Furion.Extras.Web.HttpContext              |              [![nuget](https://img.shields.io/nuget/v/Furion.Extras.Web.HttpContext.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.Web.HttpContext)              | `Furion HttpContext` expansion package  |
|     [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.ObjectMapper.Mapster)     | Furion.Extras.ObjectMapper.Mapster         |         [![nuget](https://img.shields.io/nuget/v/Furion.Extras.ObjectMapper.Mapster.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.ObjectMapper.Mapster)         | `Furion Mapster` expansion package      |
|  [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.DatabaseAccessor.SqlSugar)   | Furion.Extras.DatabaseAccessor.SqlSugar    |    [![nuget](https://img.shields.io/nuget/v/Furion.Extras.DatabaseAccessor.SqlSugar.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.DatabaseAccessor.SqlSugar)    | `Furion SqlSugar` expansion package     |
|   [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.DatabaseAccessor.Dapper)    | Furion.Extras.DatabaseAccessor.Dapper      |      [![nuget](https://img.shields.io/nuget/v/Furion.Extras.DatabaseAccessor.Dapper.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.DatabaseAccessor.Dapper)      | `Furion Dapper` expansion package       |
|       [![nuget](https://shields.io/badge/-Nuget-blue?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Extras.Logging.Serilog)        | Furion.Extras.Logging.Serilog              |              [![nuget](https://img.shields.io/nuget/v/Furion.Extras.Logging.Serilog.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Extras.Logging.Serilog)              | `Furion Serilog` expansion package      |

## 🍄 Templates

|                                                              Template Type                                                               | Name                             |                                                                               Version                                                                                | Description                |
| :--------------------------------------------------------------------------------------------------------------------------------------: | -------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------: | -------------------------- |
|       [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.Mvc/)        | Furion.Template.Mvc              |              [![nuget](https://img.shields.io/nuget/v/Furion.Template.Mvc.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.Mvc/)              | Mvc Template               |
|       [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.Api/)        | Furion.Template.Api              |              [![nuget](https://img.shields.io/nuget/v/Furion.Template.Api.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.Api/)              | WebApi Template            |
|       [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.App/)        | Furion.Template.App              |              [![nuget](https://img.shields.io/nuget/v/Furion.Template.App.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.App/)              | Mvc/WebApi Template        |
|      [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.Razor/)       | Furion.Template.Razor            |            [![nuget](https://img.shields.io/nuget/v/Furion.Template.Razor.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.Razor/)            | RazorPages Template        |
| [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.RazorWithWebApi/)  | Furion.Template.RazorWithWebApi  |  [![nuget](https://img.shields.io/nuget/v/Furion.Template.RazorWithWebApi.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.RazorWithWebApi/)  | RazorPages/WebApi Template |
|      [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.Blazor/)      | Furion.Template.Blazor           |           [![nuget](https://img.shields.io/nuget/v/Furion.Template.Blazor.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.Blazor/)           | Blazor Template            |
| [![nuget](https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800)](https://www.nuget.org/packages/Furion.Template.BlazorWithWebApi/) | Furion.Template.BlazorWithWebApi | [![nuget](https://img.shields.io/nuget/v/Furion.Template.BlazorWithWebApi.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion.Template.BlazorWithWebApi/) | Blazor/WebApi Template     |

**[How to use template](https://monksoul.gitee.io/furion/docs/template)**

## 🍎 Features

- New look: Based on `.NET 5` platform, no historical burden
- Easy to get started: only one `Inject()` is needed to complete the configuration
- Fast development: built in rich enterprise application development functions
- Very few dependencies: the framework relies on only two third-party packages
- Extremely flexible: easy to face the changing and complex needs
- Easy to maintain: adopt unique architecture idea, only designed for long-term maintenance
- Complete documentation: provide complete development documentation

## 🥞 Architecture

Sort it out later...

## 🥝 Funcions

<p align="center">
<img src="./handbook/static/img/functions.en.png"/>
</p>

## 🥐 Dependencies

`Furion` in order to pursue fast entry, the ultimate performance, as far as possible do not use or reduce third-party dependence.

At present, `Furion` only integrates the following two dependencies:

- [MiniProfiler](https://github.com/MiniProfiler/dotnet): Performance analysis and monitoring.
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)：Generating `Swagger` webapi documents.

Although sparrow is small, it has all five internal organs. `Furion` only integrates these two dependencies, but there are many mainstream ones such as `IOC/DI`,`AOP`,`EventBus`,`Data Validation` and `ORM`.

## 🥗 Requirement

- Visual Studio 2019 16.8 +
- .NET 5 SDK +
- .Net Standard 2.1 +

## 🥪 Platform

- Environment
  - Windows
  - Linux
  - MacOS
  - Docker/K8S/K3S/Rancher
  - Xamarin/MAUI
- Database
  - SqlServer
  - Sqlite
  - Azure Cosmos
  - MySql
  - MariaDB
  - PostgreSQL
  - InMemoryDatabase
  - Oracle（**Not fully adapted**）
  - Firebird
  - DM Database
- Deploy
  - Kestrel
  - Nginx
  - Jexus
  - IIS
  - Apache
  - PM2
  - Supervisor
  - SCD
  - Container（Docker/K8S/K3S/Rancher）

## 🍖 Performance

`Furion` currently uses `Visual Studio 2019 16.8` with performance test and `JMeter` for testing. Due to the limited space, only some test charts are pasted. The test results are as follows:

<img src="./handbook/static/img/xncs.png"/>

## 🌴 Stargazers

[![Stargazers over time](https://whnb.wang/img/monksoul/Furion?expire=43200)](https://whnb.wang/monksoul/Furion?expire=43200)

## 🍻 Contribution

`Furion` follows the `Apache-2.0` open source agreement. You are welcome to submit `Pull Request` or `Issue`.

If you want to contribute to a project, check out the [Contribution Guide](https://monksoul.gitee.io/furion/docs/contribute)。
