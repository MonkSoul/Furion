using Fur.Extensions;
using System;
using System.Linq;
using System.Text;

namespace Fur.MirrorController.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// 移除字符串前后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="affixs">前后缀</param>
        /// <returns>新的字符串</returns>
        internal static string ClearStringAffix(this string str, params string[] affixs)
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
    }
}