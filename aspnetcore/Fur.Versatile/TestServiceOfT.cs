using Fur.DependencyInjection.Lifetimes;

namespace Fur.Versatile
{
    public class TestServiceOfT<T> : ITestServiceOfT<T>, ITransientLifetimeOfT<T>
    {
        public string GetName()
        {
            return "Fur Of T";
        }
    }
}
