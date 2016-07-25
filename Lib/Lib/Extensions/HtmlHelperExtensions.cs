using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Lib.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString OptionList(this HtmlHelper helper, Dictionary<string, string> keyValues)
        {
            var stringBuilder = new StringBuilder();
            foreach (var keyValue in keyValues)
            {
                stringBuilder.AppendFormat("<option key={0}>{1}</option>", keyValue.Key, keyValue.Value);
            }
            return new MvcHtmlString(stringBuilder.ToString());
        }

        public static MvcHtmlString OptionList(this HtmlHelper helper, List<string> values)
        {
            var stringBuilder = new StringBuilder();
            foreach (var value in values)
            {
                stringBuilder.AppendFormat("<option key={0}>{0}</option>", value);
            }
            return new MvcHtmlString(stringBuilder.ToString());
        }

        public static MvcHtmlString List(this HtmlHelper helper, List<string> values)
        {
            if (values == null)
            {
                return new MvcHtmlString(string.Empty);
            }
            var stringBuilder = new StringBuilder();
            foreach (var value in values)
            {
                stringBuilder.AppendFormat("<li>{0}</li>", value);
            }
            return new MvcHtmlString(stringBuilder.ToString());
        }

        public static MvcHtmlString ReturnUrl(this HtmlHelper helper, string url){
            if (string.IsNullOrEmpty(url))
            {
                return new MvcHtmlString(string.Empty);
            }
            var keyValString = string.Format("{0}={1}", "return-url", HttpUtility.UrlEncode(url));
            return new MvcHtmlString(keyValString);
        }

        public static MvcHtmlString ToggleDisplay(this HtmlHelper helper, bool shouldShow)
        {
            return shouldShow ? new MvcHtmlString("style=\"display: block;\"") : new MvcHtmlString(string.Empty);
        }
		
		public static MvcHtmlString ToggleDisabled(this HtmlHelper helper, bool disabled)
        {
            return disabled ? new MvcHtmlString("disabled") : new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString ConditionalHtml(this HtmlHelper helper, object conditionValue, string html){
            if (conditionValue == null)
            {
                return new MvcHtmlString(string.Empty);
            }
            if (conditionValue is bool && (bool)conditionValue == false)
            {
                return new MvcHtmlString(string.Empty);
            }

            return new MvcHtmlString(html);
        }
    }
}