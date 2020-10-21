using Microsoft.Extensions.DependencyInjection;

namespace Fur.Core
{
    [AppStartup(800)]
    public sealed class FurCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppAuthorization<JWTAuthorizationHandler>(options =>
            {
                options.AddJWTAuthorization();
            });
        }
    }
}