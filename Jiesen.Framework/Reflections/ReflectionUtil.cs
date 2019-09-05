using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Jiesen.Framework.Reflections
{
    public static class ReflectionUtil
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _cachedProperties = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyNameFromExpression<T>(Expression<Func<T, object>> expression)
        {
            string propertyPath = null;
            if (expression.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression.Body;
                if (unaryExpression.NodeType == ExpressionType.Convert)
                    propertyPath = unaryExpression.Operand.ToString();
            }

            if (propertyPath == null)
                propertyPath = expression.Body.ToString();

            propertyPath = propertyPath.Replace(expression.Parameters[0] + ".", string.Empty);

            return propertyPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static List<string> GetPropertyNamesFromExpressions<T>(Expression<Func<T, object>>[] expressions)
        {
            var propertyNames = new List<string>();
            foreach (var expression in expressions)
            {
                var propertyName = GetPropertyNameFromExpression(expression);
                propertyNames.Add(propertyName);
            }
            return propertyNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object item, PropertyInfo property)
        {
            return property.GetValue(item, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object item, string propertyName)
        {
            PropertyInfo property;
            foreach (var part in propertyName.Split('.'))
            {
                if (item == null)
                    return null;

                var type = item.GetType();

                property = type.GetProperty(part);
                if (property == null)
                    return null;

                item = GetPropertyValue(item, property);
            }
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetPropertyValueDynamic(object item, string name)
        {
            var dictionary = (IDictionary<string, object>)item;

            return dictionary[name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, PropertyInfo> GetProperties(Type type)
        {
            var properties = _cachedProperties.GetOrAdd(type, BuildPropertyDictionary);

            return properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Dictionary<string, PropertyInfo> BuildPropertyDictionary(Type type)
        {
            var result = new Dictionary<string, PropertyInfo>();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                result.Add(property.Name.ToLower(), property);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsList(object item)
        {
            return item is ICollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsNullable(PropertyInfo property)
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;

            return false;
        }

        /// <summary>
        /// Includes a work around for getting the actual type of a Nullable type.
        /// </summary>
        public static Type GetPropertyType(PropertyInfo property)
        {
            if (IsNullable(property))
                return property.PropertyType.GetGenericArguments()[0];

            return property.PropertyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBasicClrType(Type type)
        {
            if (type.IsEnum
                || type.IsPrimitive
                || type.IsValueType
                || type == typeof(string)
                || type == typeof(DateTime))
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDynamicType(Type type)
        {
            return type.Equals(typeof(ExpandoObject)) || type.Equals(typeof(object));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Type GetGenericType(object list)
        {
            return list.GetType().GetGenericArguments()[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTypeIgoreNullable<T>(object value)
        {
            if (null == value) return false;

            Type type = value.GetType();
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];

            return type.Equals(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateNew<T>()
        {
            if (IsDynamicType(typeof(T)))
                return (T)(IDictionary<string, object>)new ExpandoObject();

            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetValue(object item, string name)
        {
            if (IsDynamicType(item.GetType()))
                return ReflectionUtil.GetPropertyValueDynamic(item, name);

            return ReflectionUtil.GetPropertyValue(item, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetValue(object item, string name, object value)
        {
            Type type = item.GetType();
            if (IsDynamicType(type))
                ((IDictionary<string, object>)item).Add(name, value);

            var property = type.GetProperty(name);
            property.SetValue(item, value, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Dictionary<string, Type> GetListProperties(dynamic list)
        {
            var type = GetGenericType(list);
            var names = new Dictionary<string, Type>();

            if (IsDynamicType(type))
            {
                if (list.Count > 0)
                    foreach (var item in GetDictionaryValues(list[0]))
                        names.Add(item.Key, (item.Value ?? string.Empty).GetType());
            }
            else
            {
                foreach (var p in GetProperties(type))
                    names.Add(p.Value.Name, p.Value.PropertyType);
            }

            return names;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetDictionaryValues(object item)
        {
            if (IsDynamicType(item.GetType()))
                return item as IDictionary<string, object>;

            var expando = (IDictionary<string, object>)new ExpandoObject();
            var properties = GetProperties(item.GetType());
            foreach (var p in properties)
                expando.Add(p.Value.Name, p.Value.GetValue(item, null));
            return expando;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="handle"></param>
        public static void EachListHeader(object list, Action<int, string, Type> handle)
        {
            var index = 0;
            var dict = GetListProperties(list);
            foreach (var item in dict)
                handle(index++, item.Key, item.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="handle"></param>
        public static void EachListRow(object list, Action<int, object> handle)
        {
            var index = 0;
            IEnumerator enumerator = ((dynamic)list).GetEnumerator();
            while (enumerator.MoveNext())
                handle(index++, enumerator.Current);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="handle"></param>
        public static void EachObjectProperty(object row, Action<int, string, object> handle)
        {
            var index = 0;
            var dict = GetDictionaryValues(row);
            foreach (var item in dict)
                handle(index++, item.Key, item.Value);
        }
    }
}
