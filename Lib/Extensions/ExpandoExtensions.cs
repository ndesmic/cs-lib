using System.Collections.Generic;
using System.Dynamic;

namespace Lib.Extensions
{
    public static class ExpandoExtensions
    {
        public static bool HasProperty(this ExpandoObject source, string name)
        {
            return source != null && (source as IDictionary<string,object>).ContainsKey(name);
        }

        public static object GetPropertyValue(this ExpandoObject source, string name)
        {
            return source.HasProperty(name) ? (source as IDictionary<string, object>)[name] : null;
        }

        public static T Query<T>(this ExpandoObject source, string accessor) 
        {
            var parts = accessor.Split('.');
            object value = source;
            foreach (var part in parts)
            {
                if (value is ExpandoObject)
                {
                    var expandoValue = value as ExpandoObject;
                    if (!expandoValue.HasProperty(part))
                    {
                        return default(T);
                    }
                    value = expandoValue.GetPropertyValue(part);
                }
                else
                {
                    return default(T);
                }
            }

            return (T)value;
        }
    }
}