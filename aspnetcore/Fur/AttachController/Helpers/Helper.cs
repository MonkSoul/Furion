using Fur.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fur.AttachController.Helpers
{
    internal class Helper
    {
        #region 移除字符串前后缀 +/* internal static string ClearStringAffix(string str, params string[] affixs)
        /// <summary>
        /// 移除字符串前后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="affixs">前后缀</param>
        /// <returns>新的字符串</returns>
        internal static string ClearStringAffix(string str, params string[] affixs)
        {
            if (!str.HasValue()) throw new ArgumentNullException(nameof(str));

            if (affixs == null || affixs.Length == 0) return str;

            var shortAffixString = affixs.OrderBy(u => u.Length).FirstOrDefault();

            bool isClearStart = false;
            bool isClearEnd = false;

            var stringBuilder = new StringBuilder();
            foreach (var affix in affixs)
            {
                if (!isClearStart && str.StartsWith(affix))
                {
                    stringBuilder.Insert(0, str.Substring(affix.Length));
                    isClearStart = true;
                }
                if (!isClearEnd && str.EndsWith(affix))
                {
                    var _tempStr = stringBuilder.Length > 0 ? stringBuilder.ToString() : str;
                    stringBuilder.Append(_tempStr.Substring(0, _tempStr.Length - affix.Length));
                    isClearEnd = true;
                }
                if (isClearStart && isClearEnd) break;
            }
            var newStr = stringBuilder.ToString();
            return newStr.HasValue() ? newStr : str;
        }
        #endregion

        #region 将字符串按照骆驼命名反射切割 +/* internal string[] CamelCaseSplitString(string str)
        /// <summary>
        /// 将字符串按照骆驼命名反射切割
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>切换后数组</returns>
        internal static string[] CamelCaseSplitString(string str)
        {
            if (!str.HasValue()) throw new ArgumentNullException(nameof(str));
            if (str.Length == 1) return new string[] { str };

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})").Where(u => u.HasValue()).ToArray();
        }
        #endregion

        #region 获取骆驼命名第一个单词 +/* internal static string GetCamelCaseFirstWord(string str)
        /// <summary>
        /// 获取骆驼命名第一个单词
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首个单词</returns>
        internal static string GetCamelCaseFirstWord(string str)
            => CamelCaseSplitString(str).FirstOrDefault();
        #endregion

        #region 是否是基元类型 +/* internal static bool IsPrimitive(Type type, bool includeEnum = true)
        /// <summary>
        /// 是否是基元类型
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="includeEnum">是否包含枚举</param>
        /// <returns>是否</returns>
        internal static bool IsPrimitive(Type type, bool includeEnum = true)
        {
            if (type.IsPrimitive) return true;
            if (includeEnum && type.IsEnum) return true;

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }
        #endregion

        #region 是否是基元类型，包括可空对象 +/* internal static bool IsPrimitiveIncludeNullable(Type type, bool includeEnum = true)
        /// <summary>
        /// 是否是基元类型，包括可空对象
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="includeEnum">是否包含枚举</param>
        /// <returns>是否</returns>
        internal static bool IsPrimitiveIncludeNullable(Type type, bool includeEnum = true)
        {
            if (IsPrimitive(type, includeEnum)) return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return IsPrimitive(type.GenericTypeArguments[0], includeEnum);

            return false;
        }
        #endregion
    }
}
