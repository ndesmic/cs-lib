using System;

namespace Lib.DbHelper
{
    public static class DbHelper
    {
        public static string GetDbValue(object value)
        {
            if (value == null)
            {
                return "null";
            }
            if (value is string || value is char)
            {
                return string.Format("'{0}'", value);
            }
            if (value is DateTime)
            {
                var date = ((DateTime)value);
                return string.Format("'{0}'", date.ToString("yyyy-dd-MM HH:mm:ss"));
            }
            if (value is Enum)
            {
                return ((int)value).ToString();
            }
            return value.ToString();
        }
    }
}