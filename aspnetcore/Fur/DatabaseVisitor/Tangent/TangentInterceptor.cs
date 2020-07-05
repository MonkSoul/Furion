using Castle.DynamicProxy;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentInterceptor : IInterceptor
    {
        private readonly TangentAsyncInterceptor _tangentAsyncInterceptor;

        public TangentInterceptor(TangentAsyncInterceptor tangentAsyncInterceptor)
        {
            _tangentAsyncInterceptor = tangentAsyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _tangentAsyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}
