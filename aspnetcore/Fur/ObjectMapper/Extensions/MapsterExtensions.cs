using Fur.AppCore.Attributes;
using Mapster;

namespace Fur.ObjectMapper.Extensions
{
    [NonInflated]
    public static class MapsterExtensions
    {
        public static TSetter ConvertUnderlineNamedToCamelCaseNamed<TSetter>(this TSetter setter, bool ignoreCase = true) where TSetter : TypeAdapterSetter
        {
            setter.Settings.NameMatchingStrategy = new NameMatchingStrategy
            {
                SourceMemberNameConverter = (string name) =>
                {
                    var _name = name.ConvertUnderlineNamedToCamelCaseNamed();
                    return ignoreCase ? _name.ToLower() : _name;
                },
                DestinationMemberNameConverter = (string name) => ignoreCase ? name.ToLower() : name
            };
            return setter;
        }

        public static TSetter ConvertCamelCaseNamedToUnderlineNamed<TSetter>(this TSetter setter, bool ignoreCase = true) where TSetter : TypeAdapterSetter
        {
            setter.Settings.NameMatchingStrategy = new NameMatchingStrategy
            {
                SourceMemberNameConverter = (string name) =>
                {
                    var _name = name.ConvertCamelCaseNamedToUnderlineNamed();
                    return ignoreCase ? _name.ToLower() : _name;
                },
                DestinationMemberNameConverter = (string name) => ignoreCase ? name.ToLower() : name,
            };
            return setter;
        }
    }
}