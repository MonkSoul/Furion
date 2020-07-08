using System;
using System.Linq.Expressions;

namespace Fur.EmitReflection
{
    public class ExpressionCreateObject
    {
        private static Func<object> func;
        public static T CreateInstance<T>() where T : class
        {
            if (func == null)
            {
                var newExpression = Expression.New(typeof(T));
                func = Expression.Lambda<Func<object>>(newExpression).Compile();
            }
            return func() as T;
        }

        public static T CreateInstance<T>(Type type) where T : class
        {
            if (func == null)
            {
                var newExpression = Expression.New(type);
                func = Expression.Lambda<Func<object>>(newExpression).Compile();
            }
            return func() as T;
        }
    }
}
