// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Xunit;

/// <summary>
/// 通过特性方式任何类型
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class AssemblyFixtureAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fixtureType"></param>
    public AssemblyFixtureAttribute(Type fixtureType)
    {
        FixtureType = fixtureType;
    }

    /// <summary>
    /// 单元测试实例构造函数修复类型
    /// </summary>
    public Type FixtureType { get; private set; }
}