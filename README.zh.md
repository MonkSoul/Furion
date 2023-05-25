**中** | [En](https://github.com/MonkSoul/Furion) | 允许商用，慷慨[赞助](https://furion.baiqian.ltd/docs/donate)

# 先知

一个应用程序框架，您可以将它集成到任何 .NET/C# 应用程序中。

## 安装

```powershell
dotnet add package Furion
```

## 例子

我们在 [主页](https://furion.baiqian.ltd) 上有不少例子，这是让您入门的第一个：

```cs
Serve.Run();

[DynamicApiController]
public class HelloService
{
    public string Say() => "Hello, Furion";
}
```

打开浏览器访问 `http://localhost:5000`。

## 文档

您可以在 [主页](https://furion.baiqian.ltd) 找到 [Furion](https://gitee.com/dotnetchina/Furion) 文档。

## 贡献

该存储库的主要目的是继续发展 [Furion](https://gitee.com/dotnetchina/Furion) 核心，使其更快、更易于使用。 [Furion](https://gitee.com/dotnetchina/Furion) 的开发在 [Gitee](https://gitee.com/dotnetchina/Furion) 上公开进行，我们感谢社区贡献错误修复和改进。

阅读 [贡献指南](https://furion.baiqian.ltd/docs/contribute) 内容，了解如何参与改进 [Furion](https://gitee.com/dotnetchina/Furion)。

## 许可证

[Furion](https://gitee.com/dotnetchina/Furion) 采用 [MIT](https://gitee.com/dotnetchina/Furion/blob/v4/LICENSE.zh) 开源许可证。
