using Microsoft.Extensions.DependencyInjection;
using System;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 创建作用域静态类
    /// </summary>
    [SkipScan]
    public static class Scoped
    {
        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <param name="handle"></param>
        public static void Create(Action<IServiceScopeFactory, IServiceScope> handle)
        {
            if (handle == null) throw new ArgumentNullException(nameof(handle));

            // 解析服务作用域工厂
            var scopeFactory = App.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            handle.Invoke(scopeFactory, scope);
        }
    }
}