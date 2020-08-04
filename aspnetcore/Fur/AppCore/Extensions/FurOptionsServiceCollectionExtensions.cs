using Fur.AppCore.Attributes;
using Fur.AppCore.Options;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [NonInflated]
    public static class FurOptionsServiceCollectionExtensions
    {
        public static IServiceCollection AddFurOptions<TOptions>(this IServiceCollection services, Action<IFurOptions> callback)
            where TOptions : class, IFurOptions
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var optionType = typeof(TOptions);

            var settings = configuration.GetSection(optionType.Name);
            services.AddOptions<TOptions>()
                .Bind(settings)
                .ValidateDataAnnotations();

            var optionInstance = settings.Get<AppOptions>();
            callback?.Invoke(optionInstance);

            return services;
        }
    }
}