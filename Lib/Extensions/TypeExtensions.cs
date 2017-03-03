using System;

namespace Lib.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }
            return false;
        }
    }
}
