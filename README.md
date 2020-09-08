<p></p>
<p></p>

<p align="center">
<img src="./handbook/static/img/furlogo.png" height="80"/>
</p>

<div align="center">

[![star](https://gitee.com/monksoul/Fur/badge/star.svg?theme=dark)](https://gitee.com/monksoul/Fur/stargazers) [![fork](https://gitee.com/monksoul/Fur/badge/fork.svg?theme=dark)](https://gitee.com/monksoul/Fur/members) ![Apache-2.0](https://img.shields.io/badge/license-Apache%202-blue)

</div>

<div align="center">

`Fur` 是 `.NET 5` 平台下极易入门、极速开发的 Web 应用框架。

</div>

## ✨快速尝鲜✨

`Fur` **是基于最新的 .NET 5 每日构建版构建的，目的是为了尽早体验新功能，做出最快的正式版前的调整。** ✈ 

所以需要运行 `Fur` 目前需要三个条件：

- 添加 Nuget 包源：dotnet5（`https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet5/nuget/v3/index.json`）
- 安装最新的 .NET 5 Preview 8：https://dotnet.microsoft.com/download/dotnet/5.0
- 升级 Visual Studio 到最新版

**[⏳ 查看Fur目前进度](https://gitee.com/monksoul/Fur/board)**

<img src="./handbook/static/img/demo.gif" />

-----

## 名字的由来

> 故事是这样子的：
>
> 起初，想开发一个极易入门的框架，开发理念为：`一切从简，只为了更懒`。
>
> 所以自然而然想到了：`Lazier`，也就是 **更懒** 的意思。但是 **更懒** 和 **更烂** 读音很相近且中文名没有特色，对此换名问题我苦恼了好几天。
>
> 刚好有一次我在博客园中帮一个博友解答问题，解决后博友夸我对 `.NET Core` 颇有了解，我就顺嘴回答了一句：**“略懂皮毛”**。
>
> 就这时，脑瓜子灵机一动，干脆起名为：**“皮毛”**？英文单词 **“`Fur` [fɜː(r)]”**，单词又短而且中文读音既俗气又顺口。😄😎
>
> 所以，**`Fur`** 就诞生了。
>
> 之后就有了 **“小僧不才，略懂皮毛（Fur）。”** 广告语 和 **[furos.cn](https://furos.cn)** 域名。

## 关于 LOGO

我相信很多人看到 `Fur` 的 LOGO 时都会问：“为什么选择奶牛？”，因为 **那些年吹过的牛逼都实现了 🐮**。

之所以选择 **奶牛** 是因为 `牛` 具有脚踏实地，任劳任怨的做事风格，同时 **奶牛** 意味着丰富的营养价值，正如 `Fur` 所能带给你的。

## 文档地址

[https://monksoul.gitee.io/fur/](https://monksoul.gitee.io/fur/) 临时的

## 开源地址

- Gitee：[https://gitee.com/monksoul/Fur](https://gitee.com/monksoul/Fur)
- GitHub：[https://github.com/monksoul/Fur](https://github.com/monksoul/Fur)
- 博客园：[https://www.cnblogs.com/furos](https://www.cnblogs.com/furos)

## 架构设计

正在整理中...

## 功能模块

<p align="center">
<img src="./handbook/static/img/furfunctions.png"/>
</p>

## 框架依赖

`Fur` 为了追求极速入门，极致性能，尽可能的不使用或减少第三方依赖。目前 `Fur` 仅集成了以下三个依赖：

- [Mapster](https://github.com/MapsterMapper/Mapster)：比 `AutoMapper` 还高性能的对象映射
- [MiniProfiler](https://github.com/MiniProfiler/dotnet)：性能分析和监听必备
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)：`Swagger` 接口文档

麻雀虽小五脏俱全。`Fur` 即使只集成了这三个依赖，但是主流的 `依赖注入/控制反转`，`AOP` 面向切面编程，`事件总线`，`数据验证`，`数据库操作` 等等一个都不少。

## 环境要求

- Visual Studio 2019 16.7 +
- .NET 5 SDK +
- .Net Standard 2.1 +

## 支持平台

- 运行环境
    - Windows
    - Linux
    - MacOS
    - Docker/K8S
- 数据库
    - SqlServer
    - Sqlite
    - Azure Cosmos
    - MySql
    - PostgreSQL
    - 内存数据库

## 谁在使用

- 百签科技（广东）有限公司
- 码为科技（广州）有限公司
- 珠海爱路达信息科技有限公司
- 珠海思诺锐创软件有限公司
- 中山赢友网络科技有限公司
- 广州启顺国际货运代理有限公司
- 森丰供应链服务（广州）有限公司
- 中山模思软件科技有限公司
- 深圳市易胜科技有限公司
- 珠海市恒泰新软件有限责任公司

## 贡献代码

`Fur` 是基于 `Apache-2.0` 开源协议的框架，欢迎大家提交 `PR` 或 `Issue`。

如果要为项目做出贡献，请查看贡献指南。
