namespace Furion.Application;

public interface ISql : ISqlDispatchProxy
{
    // 执行sql并传入参数，基元类型
    [SqlExecute("select * from person")]
    List<Person> GetPersons();
}