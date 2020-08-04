using Fur.AppCore.Attributes;
using Fur.AppCore.Options;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [NonInflated]
    public static class FurOptionsServiceCollectionExtensions
    {
        public static IServiceCollection AddFurOptions<TOptions>(this IServiceCollection services, Action<IFurOptions> callback, IConfiguration configuration)
            where TOptions : class, IFurOptions
        {
            var settings = configuration.GetSection(typeof(TOptions).Name);
            services.AddOptions<TOptions>().Bind(settings).ValidateDataAnnotations();
            callback?.Invoke(settings.Get<AppOptions>());
            return services;
        }
    }
}