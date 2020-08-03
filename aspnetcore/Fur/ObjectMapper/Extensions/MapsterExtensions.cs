using Fur.AppCore.Attributes;
using Fur.ObjectMapper.Helpers;
using Mapster;

namespace Fur.ObjectMapper.Extensions
{
    [NonWrapper]
    public static class MapsterExtensions
    {
        public static TSetter ConvertUnderlineNamedToCamelCaseNamed<TSetter>(this TSetter setter, bool ignoreCase = true) where TSetter : TypeAdapterSetter
        {
            setter.Settings.NameMatchingStrategy = new NameMatchingStrategy
            {
                SourceMemberNameConverter = (string name) =>
                {
                    var _name = Helper.ConvertUnderlineNamedToCamelCaseNamed(name);
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
                    var _name = Helper.ConvertCamelCaseNamedToUnderlineNamed(name);
                    return ignoreCase ? _name.ToLower() : _name;
                },
                DestinationMemberNameConverter = (string name) => ignoreCase ? name.ToLower() : name,
            };
            return setter;
        }
    }
}