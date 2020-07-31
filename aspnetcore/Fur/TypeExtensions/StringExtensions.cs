using Fur.AppBasic.Attributes;

namespace Fur.TypeExtensions
{
    [NonWrapper]
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}