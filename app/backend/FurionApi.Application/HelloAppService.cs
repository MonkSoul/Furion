namespace FurionApi.Application;

public class HelloAppService : IDynamicApiController
{
    public string Say()
    {
        return "Hello Furion";
    }
}