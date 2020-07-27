using Fur.DatabaseAccessor.Tangent.Dependencies;

namespace Fur.DatabaseAccessor.Tangent
{
    /// <summary>
    /// 泛型切面数据库操作上下文
    /// </summary>
    /// <typeparam name="TTangent">切面上下文接口依赖</typeparam>
    public interface ITangentDbContextOfT<TTangent> where TTangent : class, ITangentProxyDependency
    {
        /// <summary>
        /// 被代理的接口对象
        /// </summary>
        TTangent Proxy { get; }
    }
}