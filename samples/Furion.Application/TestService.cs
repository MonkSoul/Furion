using Furion.DatabaseAccessor;
using Furion.DynamicApiController;

namespace Furion.Application
{
    public class TestService : IDynamicApiController
    {
        //[UnitOfWork]
        public void Test()
        {
            "select * from person".SqlQuery();
            "update person set name='哈哈哈' where id =1".SqlNonQuery();
            "update city set name='哈哈哈' where id =1".SqlNonQuery();
        }
    }
}