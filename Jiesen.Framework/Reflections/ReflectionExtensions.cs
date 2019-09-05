using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Jiesen.Core.Extensions;

namespace Jiesen.Framework.Reflections
{
    /// <summary>
    /// 反射相关扩展
    /// </summary>
    public static class ReflectionExtensions
    {
        #region Description
        public static string GetDescription(this MemberInfo type)
        {
            var desc = type.GetCustomAttribute<DescriptionAttribute>();
            return desc == null ? null : desc.Description;
        }
        #endregion

        /// <summary>
        /// 扩展Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T GiveDefaultDateTime<T>(this T data)
        {
            data.GetType().GetProperties().ForEach(item =>
            {
                if (item.PropertyType == typeof(DateTime))
                    item.SetValue(data, DateTime.Now);
            });
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public static void ConvertStringNullToEmpty(object data)
        {
            if (data == null) return;
            data.GetType().GetProperties().ForEach(item =>
            {
                if (item.PropertyType == typeof(string) && item.GetValue(data) == null)
                    item.SetValue(data, string.Empty);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static bool Begin<T>(this T value, T start) where T : IComparable<T>
        {
            if (start == null) return false;
            return value.CompareTo(start) >= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool End<T>(this T value, T end) where T : IComparable<T>
        {
            if (end == null) return false;
            return value.CompareTo(end) <= 0;
        }


    }
}
