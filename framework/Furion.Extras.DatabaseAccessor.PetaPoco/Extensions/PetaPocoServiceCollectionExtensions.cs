using PetaPoco;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Dapper 拓展类
    /// </summary>
    public static class PetaPocoServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Dapper 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddPetaPoco(this IServiceCollection services, Action<IDatabaseBuildConfiguration> optionsAction)
        {
            services.AddTransient<IDatabase>(sp =>
            {
                var builder = DatabaseConfiguration.Build();
                optionsAction(builder);
                return new Database(builder);
            });
            return services;
        }
    }
}