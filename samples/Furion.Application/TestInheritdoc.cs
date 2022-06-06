namespace Furion.Application;

/// <inheritdoc cref="ITestInheritdoc" />
public class TestInheritdoc : ITestInheritdoc, IDynamicApiController
{
    /// <inheritdoc cref="ITestInheritdoc.GetName"/>
    public string GetName()
    {
        return "Furion";
    }

    /// <inheritdoc />
    public string GetVersion()
    {
        return "3.3.3";
    }

    /// <inheritdoc />
    public string WithParams(string name)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public string WithParams2(int id, string name)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    string ITestInheritdoc.Private(int id)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// 测试注释继承
/// </summary>
public interface ITestInheritdoc
{
    /// <summary>
    /// 获取名称
    /// </summary>
    /// <returns></returns>
    string GetName();

    /// <summary>
    /// 获取版本
    /// </summary>
    /// <returns></returns>
    string GetVersion();

    /// <summary>
    /// 携带参数
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    string WithParams(string name);

    /// <summary>
    /// 携带参数二
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    string WithParams2(int id, string name);

    /// <summary>
    /// 隐式实现接口
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    string Private(int id);
}