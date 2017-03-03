using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Lib.Mvc
{
    public class DictionaryValueProvider : IValueProvider
    {
        private readonly IDictionary<string,object> _dictionary;

        public DictionaryValueProvider(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public object GetValue(object target)
        {
            return _dictionary[target.ToString()];
        }

        public void SetValue(object target, object value)
        {
            _dictionary[target.ToString()] = value;
        }
    }
}
