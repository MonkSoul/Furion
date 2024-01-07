// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 操作符
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class OperandsAttribute : Attribute
{
}
