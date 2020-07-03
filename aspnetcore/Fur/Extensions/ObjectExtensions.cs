using System;
using System.Linq;
using System.Reflection;

namespace Fur.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPrivateValue(this object obj, string privateField)
            => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        public static T GetPrivateValue<T>(this object obj, string privateField)
            => (T)GetPrivateValue(obj, privateField);

        public static void SetProperyValue(this object obj, string properyName, object value)
        {
            var type = obj.GetType();
            var propertyInfo = type.GetProperties().FirstOrDefault(u => u.Name == properyName);
            var properyTypeValue = Convert.ChangeType(value, propertyInfo.PropertyType);
            type.GetProperty(properyName).SetValue(obj, properyTypeValue, null);
        }
    }
}
