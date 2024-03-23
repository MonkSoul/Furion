# 测试指南

## 1. 安装测试依赖

### 1.1 xUnit 安装

在单元测试项目中，使用 NuGet 安装 [xUnit](https://xunit.net/) 测试框架及相关组件：

```shell
dotnet new xunit -n Furion.UnitTests
cd Furion.UnitTests

dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.msbuild

dotnet add package Furion.Xunit;
```

### 1.2 ASP.NET Core MVC 测试支持

为了进行针对 Controllers 和 Services 的集成测试，安装 `Microsoft.AspNetCore.Mvc.Testing` 包：

```shell
dotnet add package Microsoft.AspNetCore.Mvc.Testing
```

## 2. 编写和执行单元测试

### 2.1 xUnit 单元测试

在测试项目中创建测试类，并使用 `[Fact]` 特性标记单元测试方法。xUnit 提供了丰富的断言方法用于验证预期结果。

### 2.2 ASP.NET Core MVC 集成测试

对于涉及到 ASP.NET Core MVC 控制器的测试，可以利用 `WebApplicationFactory<Startup>` 创建一个模拟的 HTTP 客户端环境来进行集成测试。

例如，创建一个测试类继承自 `WebApplicationFactory<Startup>`，然后在测试方法中使用模拟的 HTTP 客户端请求并验证响应结果。

## 3. 参考资料

有关如何使用 xUnit 编写具体的单元测试，请参考 Furion 官方文档中的单元测试章节：[https://furion.net/docs/unittest](https://furion.net/docs/unittest)

文档中涵盖了如何组织测试项目结构，如何编写针对服务和控制器的单元测试，以及如何利用 xUnit 的各种特性进行复杂的测试场景。

---

请注意，在实际项目中，您需要根据 Furion 项目的实际情况填充测试类的具体内容。上述指南只是提供了安装测试依赖和大致的测试框架使用方向，详细的实际测试代码请参考链接中的官方文档。
