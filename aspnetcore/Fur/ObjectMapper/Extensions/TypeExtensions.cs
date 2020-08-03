using System.Text;
using System.Threading;

namespace Fur.ObjectMapper.Extensions
{
    internal static class TypeExtensions
    {
        internal static string ConvertUnderlineNamedToCamelCaseNamed(this string propertyName)
        {
            if (propertyName.IndexOf('_') == -1 || !propertyName.ToUpper().Equals(propertyName) || propertyName.StartsWith('_') || propertyName.EndsWith('_')) return propertyName;

            var stringBuilder = new StringBuilder();
            var words = propertyName.Split('_');
            foreach (var word in words)
            {
                stringBuilder.Append(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()));
            }

            return stringBuilder.ToString();
        }

        internal static string ConvertCamelCaseNamedToUnderlineNamed(this string propertyName)
        {
            var stringBuilder = new StringBuilder();
            foreach (var c in propertyName)
            {
                if (char.IsUpper(c) && !propertyName.StartsWith(c)) stringBuilder.Append('_');
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString().ToUpper();
        }
    }
}