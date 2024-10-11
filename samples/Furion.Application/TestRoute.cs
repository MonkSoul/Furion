using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furion.Application
{
    [Route("IPad/[controller]")]
    //[ApiDescriptionSettings(KeepName =true)]
    public class TestRoute : IDynamicApiController
    {
        public string GetTest()
        {
            return "Hello";
        }
    }

    public class MyClass : TestRoute
    {
        public string GetXXX()
        {
            return string.Empty;
        }
    }
}
