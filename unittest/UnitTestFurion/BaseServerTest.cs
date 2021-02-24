using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFurion
{
    [TestClass]
    public abstract class BaseServerTest
    {
        WebServer server;
        [TestInitialize]
        public  Task InitializeAsync()
        {
            server = new WebServer();
            return server.BaseServerRunAsync();
        }
        [TestCleanup]
        public void Cleanup()
        {
            server.Dispose();
        }
    }
}
