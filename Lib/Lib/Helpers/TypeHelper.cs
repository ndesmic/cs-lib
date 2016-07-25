using System;
using System.Linq;
using System.Collections.Generic;

namespace Lib.Helpers
{
    public static class TypeHelper
    {
        public static bool IsDefault(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            return type.IsValueType && Activator.CreateInstance(type).Equals(value);
        }

        public static List<Type> GetAllTypesInheritedFromType<T>()
        {
            var type = typeof(T);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)).ToList();
        }
    }
}