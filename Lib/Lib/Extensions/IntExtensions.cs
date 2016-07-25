using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Lib.Extensions
{
    public static class IntExtensions
    {
        public static T ToEnum<T>(this int self) where T : struct
        {
            return ParseEnum(self, default(T));
        }

        public static T ParseEnum<T>(int input, T defaultValue, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Invalid Enum Type. " + typeof(T).ToString() + "  must be an Enum");
            }
            if (Enum.IsDefined(typeof(T), input))
            {
                returnEnum = (T)Enum.ToObject(typeof(T), input);
            }
            else
            {
                if (throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }

            return returnEnum;

        }

        public static bool ToBool(this int value)
        {
            return value == 0;
        }

        public static int Bucket(int value, params int[] buckets)
        {
            for (var i = 0; i < buckets.Length; i++)
            {
                if ((i == 0 && value < buckets[i]) || (i > 0 && value >= buckets[i - 1] && value < buckets[i]))
                {
                    return i;
                }
            }
            return buckets.Length;
        }
    }
}