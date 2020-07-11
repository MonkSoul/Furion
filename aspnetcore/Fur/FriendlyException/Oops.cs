using System;

namespace Fur.FriendlyException
{
    public class Oops
    {
        // [2000,false] 格式
        public static Exception Set(string code, bool upToHandle = false)
            => new Exception($"##{code}##" + (upToHandle ? $"**{upToHandle}**" : ""));

        public static Exception Set(int code, bool upToHandle = false)
           => new Exception($"##{code}##" + (upToHandle ? $"**{upToHandle}**" : ""));
    }
}
