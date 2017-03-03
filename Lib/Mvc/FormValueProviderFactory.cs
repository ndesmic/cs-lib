using System;
using System.Collections.Generic;
using System.Web;
using Lib.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace Lib.Mvc
{
    public class FormValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var contentType = httpContext.Request.Content.Headers?.ContentType?.MediaType;
            if (contentType != null && contentType == "application/x-www-form-urlencoded")
            {
                return new DictionaryValueProvider(AssembleDictionary(httpContext.Request.Content.ReadAsStringAsync().Result)) as IValueProvider;
            }
            return new DictionaryValueProvider(new Dictionary<string, object>()) as IValueProvider;
        }

        private Dictionary<string, object> AssembleDictionary(string form)
        {
            var keyvals = form.Split('&');
            var dictionary = new Dictionary<string, object>();
            foreach (var keyval in keyvals)
            {
                var keyvalSplit = keyval.Split('=');
                var key = HttpUtility.UrlDecode(keyvalSplit[0]);
                var value = HttpUtility.UrlDecode(keyvalSplit[1]);
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] += $",{value}";
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
            return dictionary;
        }
    }
}