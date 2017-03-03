using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages.Html;

namespace Lib.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString OptionList(this HtmlHelper helper, Dictionary<string, string> keyValues)
        {
            var stringBuilder = new StringBuilder();
            foreach (var keyValue in keyValues)
            {
                stringBuilder.AppendFormat("<option key={0}>{1}</option>", keyValue.Key, keyValue.Value);
            }
            return new HtmlString(stringBuilder.ToString());
        }

        public static HtmlString OptionList(this HtmlHelper helper, List<string> values)
        {
            var stringBuilder = new StringBuilder();
            foreach (var value in values)
            {
                stringBuilder.AppendFormat("<option key={0}>{0}</option>", value);
            }
            return new HtmlString(stringBuilder.ToString());
        }

        public static HtmlString List(this HtmlHelper helper, List<string> values)
        {
            if (values == null)
            {
                return new HtmlString(string.Empty);
            }
            var stringBuilder = new StringBuilder();
            foreach (var value in values)
            {
                stringBuilder.AppendFormat("<li>{0}</li>", value);
            }
            return new HtmlString(stringBuilder.ToString());
        }

        public static HtmlString ReturnUrl(this HtmlHelper helper, string url){
            if (string.IsNullOrEmpty(url))
            {
                return new HtmlString(string.Empty);
            }
            var keyValString = string.Format("{0}={1}", "return-url", HttpUtility.UrlEncode(url));
            return new HtmlString(keyValString);
        }

        public static HtmlString ToggleDisplay(this HtmlHelper helper, bool shouldShow)
        {
            return shouldShow ? new HtmlString("style=\"display: block;\"") : new HtmlString(string.Empty);
        }
		
		public static HtmlString ToggleDisabled(this HtmlHelper helper, bool disabled)
        {
            return disabled ? new HtmlString("disabled") : new HtmlString(string.Empty);
        }

        public static HtmlString ConditionalHtml(this HtmlHelper helper, object conditionValue, string html){
            if (conditionValue == null)
            {
                return new HtmlString(string.Empty);
            }
            if (conditionValue is bool && (bool)conditionValue == false)
            {
                return new HtmlString(string.Empty);
            }

            return new HtmlString(html);
        }
    }
}