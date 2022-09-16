namespace Furion.Application;

public interface ISql : ISqlDispatchProxy
{
    // 执行sql并传入参数，基元类型
    [SqlExecute("select * from person")]
    List<Person> GetPersons();

    [SqlExecute("update person set age = 30 where id = {id}", RowEffects = true)]
    int UpdatePerson(int id);

    [SqlExecute("update person set age = 30 where id = {id}", RowEffects = true)]
    Task<int> UpdatePerson2(int id);
}