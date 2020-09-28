using Fur.DependencyInjection;
using Fur.UnifyResult;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.Web.Core
{
    [Startup(800)]
    public sealed class FurWebCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpecificationDocuments();
            services.AddControllers()
                       .AddDynamicApiControllers()
                       .AddDataValidation()
                       .AddFriendlyException()
                       .AddUnifyResult<RESTfulResult, RESTfulResultProvider>();
        }
    }
}