[中](https://gitee.com/dotnetchina/Furion) | **En**

# Furion

An application framework that you can integrate into any .NET/C# application.

## Installation

```powershell
dotnet add package Furion
```

## Examples

We have several examples [on the website](https://furion.net). Here is the first one to get you started:

```cs
Serve.Run();

[DynamicApiController]
public class HelloService
{
    public string Say() => "Hello, Furion";
}
```

Open browser access `http://localhost:5000`.

## Documentation

You can find the [Furion](https://gitee.com/dotnetchina/Furion) documentation [on the website](https://furion.net).

## Contributing

The main purpose of this repository is to continue evolving [Furion](https://gitee.com/dotnetchina/Furion) core, making it faster and easier to use. Development of [Furion](https://gitee.com/dotnetchina/Furion) happens in the open on [Gitee](https://gitee.com/dotnetchina/Furion), and we are grateful to the community for contributing bugfixes and improvements.

Read [contribution documents](https://gitee.com/dotnetchina/Furion/blob/v4/CONTRIBUTING.md) to learn how you can take part in improving [Furion](https://gitee.com/dotnetchina/Furion).

## License

[Furion](https://gitee.com/dotnetchina/Furion) is primarily distributed under the terms of both the MIT license and the Apache License (Version 2.0).

See [LICENSE-APACHE](https://gitee.com/dotnetchina/Furion/blob/v4/LICENSE-APACHE), [LICENSE-MIT](https://gitee.com/dotnetchina/Furion/blob/v4/LICENSE-MIT), and [COPYRIGHT](https://gitee.com/dotnetchina/Furion/blob/v4/COPYRIGHT) for details.