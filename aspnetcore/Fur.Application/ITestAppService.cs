using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fur.Application
{
    public interface ITestAppService
    {
        Task<string> GetName1();

        Task<string> GetByName1([FromQuery] string name);

        Task<string> GetByName1([FromQuery] string name, int id);

        Task<string> GetTestName1(string name);

        Task<string> PostName1(TestModel1 testModel1);

        Task<string> PostByName1(TestModel1 testModel1);

        Task<string> PostTestName1(TestModel1 testModel1);

        Task<string> PostTestName1(string name);



        Task<string> GetName2();

        Task<string> GetByName2(string name);

        Task<string> GetTestName2(string name);

        Task<string> PostName2(TestModel2 testModel2);

        Task<string> PostByName2(TestModel2 testModel2);

        Task<string> PostTestName2(TestModel2 testModel2);

        Task<string> GetName3();

        Task<string> GetByName3(string name);

        Task<string> GetTestName3(string name);

        Task<string> PostName3(TestModel3 testModel3);

        Task<string> PostByName3(TestModel3 testModel3);

        Task<string> PostTestName3(TestModel3 testModel3);


        Task<string> GetName4();

        Task<string> GetByName4(string name);

        Task<string> GetTestName4(string name);

        Task<string> PostName4(TestModel4 testModel4);

        Task<string> PostByName4(TestModel4 testModel4);

        Task<string> PostTestName4(TestModel4 testModel4);


        Task<string> GetName5();

        Task<string> GetByName5(string name);

        Task<string> GetTestName5(string name);

        Task<string> PostName5(TestModel5 testModel5);

        Task<string> PostByName5(TestModel5 testModel5);

        Task<string> PostTestName5(TestModel5 testModel5);


        Task<string> GetName6();

        Task<string> GetByName6(string name);

        Task<string> GetTestName6(string name);

        Task<string> PostName6(TestModel6 testModel6);

        Task<string> PostByName6(TestModel6 testModel6);

        Task<string> PostTestName6(TestModel6 testModel6);


        Task<string> GetName7();

        Task<string> GetByName7(string name);

        Task<string> GetTestName7(string name);

        Task<string> PostName7(TestModel7 testModel7);

        Task<string> PostByName7(TestModel7 testModel7);

        Task<string> PostTestName7(TestModel7 testModel7);


        Task<string> GetName8();

        Task<string> GetByName8(string name);

        Task<string> GetTestName8(string name);

        Task<string> PostName8(TestModel8 testModel8);

        Task<string> PostByName8(TestModel8 testModel8);

        Task<string> PostTestName8(TestModel8 testModel8);




        Task<string> GetName9();

        Task<string> GetByName9(string name);

        Task<string> GetTestName9(string name);

        Task<string> PostName9(TestModel9 testModel9);

        Task<string> PostByName9(TestModel9 testModel9);

        Task<string> PostTestName9(TestModel9 testModel9);


        Task<string> GetName10();

        Task<string> GetByName10(string name);

        Task<string> GetTestName10(string name);

        Task<string> PostName10(TestModel10 testModel10);

        Task<string> PostByName10(TestModel10 testModel10);

        Task<string> PostTestName10(TestModel10 testModel10);
    }
}
