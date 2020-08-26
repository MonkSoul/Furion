using Fur.DynamicApiController;
using Fur.FriendlyException;
using System;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        [IfException(ErrorCodes.x1000, args: "百小僧")]
        public string Get()
        {
            throw Oops.Made(ErrorCodes.x1000, typeof(InvalidOperationException));

            return nameof(Fur);
        }

        public int Get(int id)
        {
            return id;
        }
    }
}