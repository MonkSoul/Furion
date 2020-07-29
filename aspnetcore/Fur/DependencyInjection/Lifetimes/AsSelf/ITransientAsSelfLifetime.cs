namespace Fur.DependencyInjection.Lifetimes.AsSelf
{
    /// <summary>
    /// 暂时生命周期依赖接口，注册为本身实例
    /// </summary>
    public interface ITransientAsSelfLifetime { }

    /// <summary>
    /// 暂时生命周期依赖接口，注册为本身实例
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public interface ITransientAsSelfLifetime<T> { }
}