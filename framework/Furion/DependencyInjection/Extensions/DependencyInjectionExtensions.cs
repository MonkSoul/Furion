namespace Furion.DependencyInjection.Extensions
{
    /// <summary>
    /// 依赖注入拓展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TService GetService<TService>(this object obj)
            where TService : class
        {
            return App.GetService<TService>();
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetRequiredService<TService>(this object obj)
            where TService : class
        {
            return App.GetRequiredService<TService>();
        }
    }
}