using System.Collections.Generic;
using System.Linq;
using Lib.DataTypes;

namespace Lib.Extensions
{
    public static class DictionaryExtensions
    {
        public static Option<T> GetByKeyCaseInvariant<T>(this IDictionary<string, T> dictionary, string key)
        {
            var invariantKey = dictionary.Keys.FirstOrDefault(k => k.ToLowerInvariant() == key.ToLowerInvariant());
            if (invariantKey == null)
            {
                return Option<T>.None;
            }
            return Option<T>.Some(dictionary[invariantKey]);
        }

        public static Option<T> GetByKey<T>(this IDictionary<string, T> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return Option<T>.Some(dictionary[key]);
            }
            return Option<T>.None;
        }

        public static Dictionary<string, T> Pick<T>(this IDictionary<string, T> dictionary, params string[] keys)
        {
            var subDictionary = new Dictionary<string, T>();
            foreach (var key in keys)
            {
                dictionary.GetByKey(key).AndThen(x => subDictionary.Add(key, x));
            }
            return subDictionary;
        }
    }
}
