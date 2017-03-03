using System;
using System.Collections.Generic;

namespace Lib.Helpers
{
    public static class SqlHelper
    {
        public static string MakeVarList(object query)
        {
            var list = string.Empty;
            foreach (var prop in query.GetType().GetProperties())
            {
                var value = prop.GetValue(query);
                list += $"declare @{prop.Name} as {GetSqlType(prop.PropertyType)} = {GetSqlValue(value)};\n";
            }
            return list;
        }

        public static string MakeDebugQuery(string sqlQuery, object query)
        {
            var runnableQuery = string.Empty;
            runnableQuery += MakeVarList(query);
            runnableQuery += "\n\n";
            runnableQuery += sqlQuery;
            return runnableQuery;
        }

        private static string GetSqlType(Type t)
        {
            var typeMap = new Dictionary<Type, string> {
                { typeof(int), "int" },
                { typeof(string), "nvarchar(max)" },
                { typeof(bool), "bit" }
            };
            if (!t.IsPrimitive && t != typeof(string))
            {
                t = Nullable.GetUnderlyingType(t);
            }
            return typeMap[t];
        }

        public static string GetSqlValue(object value)
        {
            if (value == null)
            {
                return "null";
            }
            if (value is string || value is char)
            {
                return $"'{value}'";
            }
            if (value is DateTime)
            {
                var date = ((DateTime)value);
                return $"'{date.ToString("yyyy-dd-MM HH:mm:ss")}'";
            }
            if (value is Enum)
            {
                return ((int)value).ToString();
            }
            return value.ToString();
        }
    }
}
