using Furion.DynamicApiController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFurion.RemoteRequest.Extensions
{
    public class RemoteApiServiceTestController : IDynamicApiController
    {
        public string Get()
        {
            return $"Hello {nameof(Furion)}";
        }
        public string GetGeneric()
        {
            return "{\"ip\":\"112.64.53.113\",\"port\":4275,\"expire_time\":\"2021-02-23 17:56:28\"}";
        }
    }
    public class ResponseViewModel
    {
        public string ip { get; set; }
        public int port { get; set; }
        public DateTime expire_time { get; set; }
    }
}
