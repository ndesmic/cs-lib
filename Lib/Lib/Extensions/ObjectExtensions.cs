using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Lib.Extensions
{
    public static class ObjectExtensions
    {
        public static dynamic ToExpando(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
		
		public static T MapTo<T>(this object fromObject, string prefix, string[] requiredProps) where T : class, new()
        {
            var toObject = Activator.CreateInstance<T>();
            var addressPropertyInfos = toObject.GetType().GetProperties();
            var objectPropertyInfos = fromObject.GetType().GetProperties();

            if (!objectPropertyInfos.Any(x => requiredProps.Contains(x.Name)))
            {
                return null;
            }

            foreach (var addressPropertyInfo in addressPropertyInfos)
            {
                var objectPropertyInfo = objectPropertyInfos.FirstOrDefault(x => x.Name == prefix + addressPropertyInfo.Name);
                if (objectPropertyInfo != null)
                {
                    addressPropertyInfo.SetValue(toObject, objectPropertyInfo.GetValue(fromObject));
                }
            }

            return toObject;
        }
		
		public static List<T> ToList<T>(this object value)
        {
            return new List<T> { (T)value };
        }
		
		public static object GetPrefixedProperty<T>(this T entity, string propertyName, string prefix)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name == prefix + propertyName);
            if (property != null)
            {
                return property.GetValue(entity, null);
            }
            return null;
        }
    }
}