namespace Furion.Application.Services;

public class BusinessService : IBusinessService, ITransient
{
    public string GetName()
    {
        return "我是：" + nameof(BusinessService);
    }
}
public class OtherBusinessService : IBusinessService, ITransient
{
    public string GetName()
    {
        return "我是：" + nameof(OtherBusinessService);
    }
}