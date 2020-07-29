namespace Fur.DependencyInjection.Lifetimes.AsSelf
{
    /// <summary>
    /// 作用域生存期服务依赖接口，注册为本身实例
    /// </summary>
    public interface IScopedAsSelfLifetime { }

    /// <summary>
    /// 作用域生存期服务依赖接口，注册为本身实例
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public interface IScopedAsSelfLifetime<T> { }
}