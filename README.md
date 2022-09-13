[中](https://gitee.com/dotnetchina/Furion) | **En**

# Furion

[![木兰社区](https://img.shields.io/badge/Mulan-incubating-blue)](https://portal.mulanos.cn/) [![license](https://img.shields.io/badge/license-MIT-orange?cacheSeconds=10800)](https://gitee.com/dotnetchina/Furion/blob/v4/LICENSE) [![nuget](https://img.shields.io/nuget/v/Furion.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Furion) [![nuget downloads](https://img.shields.io/badge/downloads-3.2M-green?cacheSeconds=10800)](https://www.nuget.org/profiles/monk.soul) [![dotNET China](https://img.shields.io/badge/organization-dotNET%20China-yellow?cacheSeconds=10800)](https://gitee.com/dotnetchina)

An application framework that you can integrate into any .NET/C# application.

## Installation

```powershell
dotnet add package Furion
```

## Examples

We have several examples [on the website](https://dotnetchina.gitee.io/furion). Here is the first one to get you started:

```cs
Serve.Run();

[DynamicApiController]
public class HelloService
{
    public string Say() => "Hello, Furion";
}
```

Open browser access `https://localhost:5001` or `http://localhost:5000`.

## Documentation

You can find the [Furion](https://gitee.com/dotnetchina/Furion) documentation [on the website](https://dotnetchina.gitee.io/furion) or [on the backup website](https://furion.icu).

## Contributing

The main purpose of this repository is to continue evolving [Furion](https://gitee.com/dotnetchina/Furion) core, making it faster and easier to use. Development of [Furion](https://gitee.com/dotnetchina/Furion) happens in the open on [Gitee](https://gitee.com/dotnetchina/Furion), and we are grateful to the community for contributing bugfixes and improvements.

Read [contribution documents](https://dotnetchina.gitee.io/furion/docs/contribute) to learn how you can take part in improving [Furion](https://gitee.com/dotnetchina/Furion).

## License

[Furion](https://gitee.com/dotnetchina/Furion) uses the [MIT](https://gitee.com/dotnetchina/Furion/blob/v4/LICENSE) open source license.
