using Fur.MirrorController.Attributes;
using Fur.MirrorController.Dependencies;

namespace Fur.Application.Functions
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [MirrorController]
    public class TestAppService : ITestAppService, IMirrorControllerDependency
    {
    }
}