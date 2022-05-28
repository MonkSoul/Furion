using Furion.DynamicApiController;

namespace Furion.Application
{
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
    }
}
