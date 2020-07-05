using System;

namespace Fur.FriendlyException
{
    public class Oops
    {
        public static Exception Set(string code)
            => new Exception($"##{code}##");

        public static Exception Set(int code)
           => new Exception($"##{code}##");
    }
}
