using System;
using System.Linq;
using System.Collections.Generic;

namespace Lib.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Interleave<T>(this IEnumerable<T> first, params IEnumerable<T>[] others)
        {
            var queues = new[] { first }.Concat(others).Select(x => new Queue<T>(x)).ToList();
            while (queues.Any(x => x.Any()))
            {
                foreach (var queue in queues.Where(x => x.Any()))
                {
                    yield return queue.Dequeue();
                }
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TSource> MaxBy<TSource, TKey, TKey2>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey2> valueSelector)
        {
            return source.GroupBy(keySelector).Select(g => g.OrderByDescending(valueSelector).First());
        }
    }
}