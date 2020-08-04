using Fur.Attributes;

namespace Fur.Extensions
{
    [NonInflated]
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}