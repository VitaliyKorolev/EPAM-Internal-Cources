using System;
using System.Collections.Generic;

namespace LinqTasksReinvent
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerator = source.GetEnumerator();
            try
            {
                enumerator.MoveNext();
                if (enumerator.MoveNext())
                {
                    throw new ArgumentException("Sourse count should be equal to 1");
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            List<TSource> result = new List<TSource>();
            for(int i = 0; i < count; i++)
            {
                foreach(TSource el in source)
                {
                    result.Add(el);
                }
            }
            return result;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            List<TSource> result = new List<TSource>(first);
            foreach(var el in second)
            {
                result.Add(el);
            }
            return result;
        }

        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            List<TSource> result = new List<TSource>();

            var enumerator = source.GetEnumerator();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (enumerator.MoveNext())
                    {
                        result.Add(enumerator.Current);
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            return result;
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach(var el in source)
            {
                if (predicate(el))
                    return el;
            }
            return default(TSource);
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            return new List<TSource>(source);
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            List<TSource> result = new List<TSource>();
            int index = 0;
            foreach (var el in source)
            {
                if(predicate(el, index))
                result.Add(el);
                index++;
            }
            return result;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var el in source)
            {
                if (predicate(el))
                    return true;
            }
            return false;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value,
            IEqualityComparer<TSource> comparer)
        {
            foreach (var el in source)
            {
                if (comparer.Equals(el, value))
                    return true;
            }
            return false;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            List<TResult> result = new List<TResult>();
            int index = 0;
            foreach (var el in source)
            {
                result.Add(selector(el, index));
                index++;
            }
            return result;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            List<TResult> result = new List<TResult>();
            foreach (var el in source)
            {
                foreach (var elem in selector(el))
                {
                    result.Add(elem);
                }
            }
            return result;
        }

        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            var enumerator = source.GetEnumerator();
            TSource result;
            try
            {
                enumerator.MoveNext();
                result = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    result = func(result, enumerator.Current);
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            return result;
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            List<TSource> result = new List<TSource>();
            foreach(var el in source)
            {
                if (!result.Contains(el))
                {
                    result.Add(el);
                }
            }
            return result;
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            List<TSource> result = new List<TSource>();
            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            foreach (var el in first)
            {
                if (second.Contains(el, comparer))
                {
                    result.Add(el);
                }
            }
            return result;
        }

        //public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source,
        //    Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
        //    Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        //{
        //    IEqualityComparer<TKey> comparer = EqualityComparer<TKey>.Default;
        //    List<TResult> result = new List<TResult>();
        //    List<TSource> elementsWereSeen = new();
        //    foreach (var el in source)
        //    {
        //        if (!elementsWereSeen.Contains(el))
        //        {
        //            List<TElement> group = new();
        //            TKey key = keySelector(el);
        //            foreach (var elem in source)
        //            {
        //                if (comparer.Equals(key, keySelector(elem)))
        //                {
        //                    group.Add(elementSelector(elem));
        //                }
        //            }
        //            elementsWereSeen.Add(el);
        //            result.Add(resultSelector(key, group));
        //        }
        //    }
        //    return result;
        //}
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            IEqualityComparer<TKey> comparer = EqualityComparer<TKey>.Default;
            List<TResult> result = new List<TResult>();
            List<TSource> elementsWereNotSeen = source.ToList();
            while (elementsWereNotSeen.Count != 0)
            {
                List<TElement> group = new();
                TKey key = keySelector(elementsWereNotSeen[0]);
                for (int j = 0; j < elementsWereNotSeen.Count; j++)
                {
                    if (comparer.Equals(key, keySelector(elementsWereNotSeen[j])))
                    {
                        group.Add(elementSelector(elementsWereNotSeen[j]));
                        elementsWereNotSeen.RemoveAt(j);
                        j--;
                    }
                }
                result.Add(resultSelector(key, group));
            }
            return result;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            IEqualityComparer<TKey> comparer = EqualityComparer<TKey>.Default;
            List<TResult> result = new List<TResult>();
            foreach (var outerEl in outer)
            {
                TKey outerKey = outerKeySelector(outerEl);
                foreach (var innerEl in inner)
                {
                    if (comparer.Equals(outerKey, innerKeySelector(innerEl)))
                        result.Add(resultSelector(outerEl, innerEl));
                }
            }
            return result;
        }
    }
}