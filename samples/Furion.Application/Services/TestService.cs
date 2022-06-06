namespace Furion.Application.Services;

public class TestService : ITestService, ITransient
{
    public string GetName()
    {
        return "Furion";
    }
}