using Furion.Application.Services;

namespace Furion.Application;

public class TestNamedServices : IDynamicApiController
{
    private readonly INamedServiceProvider<IBusinessService> _namedServiceProvider;
    public TestNamedServices(INamedServiceProvider<IBusinessService> namedServiceProvider)
    {
        _namedServiceProvider = namedServiceProvider;
    }

    public string GetName()
    {
        // 第一种用法，通过反射解析服务周期，性能有损耗
        var service1 = _namedServiceProvider.GetService(nameof(BusinessService));
        var service2 = _namedServiceProvider.GetService(nameof(OtherBusinessService));

        // 第二种用法，无需反射
        var service3 = _namedServiceProvider.GetService<ITransient>(nameof(BusinessService));
        var service4 = _namedServiceProvider.GetService<ITransient>(nameof(OtherBusinessService));

        return service1.GetName() + "-" + service2.GetName() + "-" + service3.GetName() + "-" + service4.GetName();
    }
}