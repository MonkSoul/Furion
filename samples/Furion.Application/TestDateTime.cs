using Furion.Application.Persons;

namespace Furion.Application;

public class TestDateTime : IDynamicApiController
{
    /// <summary>
    /// 测试 08/06/2022 10:45 AM
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns></returns>
    public DateTime 测试时间1([FromConvert] DateTime datetime)
    {
        return datetime;
    }

    /// <summary>
    /// 测试其他类型
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public PersonDto 测试其他类型([FromConvert] PersonDto dto)
    {
        return dto;
    }

    public DateTime 测试序列化(DateTime datetime)
    {
        return datetime;
    }
}