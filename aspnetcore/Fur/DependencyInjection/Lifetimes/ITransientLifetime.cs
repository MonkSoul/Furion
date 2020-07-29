namespace Fur.DependencyInjection.Lifetimes
{
    /// <summary>
    /// 暂时生命周期依赖接口
    /// </summary>
    public interface ITransientLifetime { }

    /// <summary>
    /// 暂时生命周期依赖接口
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public interface ITransientLifetime<T> { }
}