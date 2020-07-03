using System;
using System.Collections.Generic;

namespace Fur.DatabaseVisitor.Page
{
    public static class IEnumerablePagedListExtensions
    {
        public static IPagedListOfT<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0) => new PagedListOfT<T>(source, pageIndex, pageSize, indexFrom);

        public static IPagedListOfT<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0) => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
    }
}