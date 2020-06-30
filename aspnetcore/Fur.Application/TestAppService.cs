using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Fur.Application
{
    [AttachController("Test", ApiVersion = "v1.0.0")]
    public class TestAppService : ITestAppService, IAttachControllerDependency
    {
        /// <summary>
        /// 测试注释
        /// </summary>
        /// <param name="name">测试</param>
        /// <param name="id">测试</param>
        /// <param name="age">测试</param>
        /// <returns></returns>
        [AttachAction(ApiVersion = "v1.0.0.1")]
        public Task<string> GetByName1([FromQuery] string name, int id, [FromRoute] int age)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName10(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName2(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName3(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName4(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName5(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName6(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName7(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName8(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetByName9(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName1()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName10()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName2()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName3()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName4()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName5()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName6()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName7()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName8()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetName9()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName1(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName10(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName2(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName3(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName4(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName5(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName6(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName7(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName8(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTestName9(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName1(TestModel1 testModel1)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName10(TestModel10 testModel10)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName2(TestModel2 testModel2)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName3(TestModel3 testModel3)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName4(TestModel4 testModel4)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName5(TestModel5 testModel5)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName6(TestModel6 testModel6)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName7(TestModel7 testModel7)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName8(TestModel8 testModel8)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostByName9(TestModel9 testModel9)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName1(TestModel1 testModel1)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName10(TestModel10 testModel10)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName2(TestModel2 testModel2)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName3(TestModel3 testModel3)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName4(TestModel4 testModel4)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName5(TestModel5 testModel5)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName6(TestModel6 testModel6)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName7(TestModel7 testModel7)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName8(TestModel8 testModel8)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostName9(TestModel9 testModel9)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName1(TestModel1 testModel1)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName10(TestModel10 testModel10)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName2(TestModel2 testModel2)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName3(TestModel3 testModel3)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName4(TestModel4 testModel4)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName5(TestModel5 testModel5)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName6(TestModel6 testModel6)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName7(TestModel7 testModel7)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName8(TestModel8 testModel8)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostTestName9(TestModel9 testModel9)
        {
            throw new System.NotImplementedException();
        }
    }
}
