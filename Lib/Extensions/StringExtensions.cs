using System.Globalization;
using System.Text.RegularExpressions;

namespace Lib.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string text)
        {
            return int.Parse(text);
        }
		
		public static string[] SplitCamelCase(string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
		
		public static string CondenseSpaces(this string text)
        {
            return Regex.Replace(text, "\\s{2,}", " ");
        }

        public static string ToKebabFormat(this string text)
        {
            return text.CondenseSpaces().Replace(" ", "-").ToLower();
        }

        public static string ToTitleCaseFormat(this string text)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            text = textInfo.ToTitleCase(text);
            return text.CondenseSpaces().Replace(" ", string.Empty);
        }

        public static string NormalizeBreaks(this string text)
        {
            return text.Replace("\r\n", "\n");
        }

        public static string ToSingleLine(this string text)
        {
            return NormalizeBreaks(text).Replace("\n", " ").CondenseSpaces();
        }
    }
}