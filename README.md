# 先知 / Furion ([探索版](https://gitee.com/dotnetchina/Furion/tree/experimental/))

[![木兰社区](https://img.shields.io/badge/Mulan-incubating-blue)](https://portal.mulanos.cn/) [![license](https://img.shields.io/badge/license-MIT-orange?cacheSeconds=10800)](https://gitee.com/dotnetchina/Furion/blob/net6/LICENSE) [![nuget](https://img.shields.io/nuget/v/Furion.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion) [![nuget downloads](https://img.shields.io/badge/downloads-2.9M-green?cacheSeconds=10800)](https://www.nuget.org/profiles/monk.soul) [![dotNET China](https://img.shields.io/badge/organization-dotNET%20China-yellow?cacheSeconds=10800)](https://gitee.com/dotnetchina)

一个应用程序框架，您可以将它集成到任何 .NET/C# 应用程序中。

An application framework that you can integrate into any .NET/C# application.

## 安装 / Installation

- [Package Manager](https://www.nuget.org/packages/Furion)

```powershell
Install-Package Furion
```

- [.NET CLI](https://www.nuget.org/packages/Furion)

```powershell
dotnet add package Furion
```

## 例子 / Examples

我们在 [主页](https://dotnetchina.gitee.io/furion) 上有不少例子，这是让您入门的第一个：

We have several examples [on the website](https://dotnetchina.gitee.io/furion). Here is the first one to get you started:

```cs
Serve.Run();

[DynamicApiController]
public class HelloService
{
    public string Say() => "Hello, Furion";
}
```

打开浏览器访问 `https://localhost:5001` 或 `http://localhost:5000`。

Open browser access `https://localhost:5001` or `http://localhost:5000`.

## 文档 / Documentation

您可以在 [主页](https://dotnetchina.gitee.io/furion) 或 [备份主页](https://furion.icu) 找到 [Furion](https://gitee.com/dotnetchina/Furion) 文档。

You can find the [Furion](https://gitee.com/dotnetchina/Furion) documentation [on the website](https://dotnetchina.gitee.io/furion) or [on the backup website](https://furion.icu).

## 贡献 / Contributing

该存储库的主要目的是继续发展 [Furion](https://gitee.com/dotnetchina/Furion) 核心，使其更快、更易于使用。 [Furion](https://gitee.com/dotnetchina/Furion) 的开发在 [Gitee](https://gitee.com/dotnetchina/Furion) 上公开进行，我们感谢社区贡献错误修复和改进。

阅读 [贡献指南](https://dotnetchina.gitee.io/furion/docs/contribute) 内容，了解如何参与改进 [Furion](https://gitee.com/dotnetchina/Furion)。

The main purpose of this repository is to continue evolving [Furion](https://gitee.com/dotnetchina/Furion) core, making it faster and easier to use. Development of [Furion](https://gitee.com/dotnetchina/Furion) happens in the open on [Gitee](https://gitee.com/dotnetchina/Furion), and we are grateful to the community for contributing bugfixes and improvements.

Read [contribution documents](https://dotnetchina.gitee.io/furion/docs/contribute) to learn how you can take part in improving [Furion](https://gitee.com/dotnetchina/Furion).

## 许可证 / License

[Furion](https://gitee.com/dotnetchina/Furion) 采用 [MIT](https://gitee.com/dotnetchina/Furion/blob/net6/LICENSE) 开源许可证。

[Furion](https://gitee.com/dotnetchina/Furion) uses the [MIT](https://gitee.com/dotnetchina/Furion/blob/net6/LICENSE) open source license.

```
MIT 许可证

版权 (c) 2020-2022 百小僧, Baiqian Co.,Ltd 和贡献者们.

特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，以及再授权被配发了本软件的人如上的权利，须在下列条件下：

上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。

本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，还是产生于、源于或有关于本软件以及本软件的使用或其它处置。
```

```
MIT License

Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
