using Fur.DataValidation;
using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        [NonVaildate] // 跳过全局验证
        public DataValidationResult Post(TestDto testDto)
        {
            return testDto.TryValidate();
        }
    }
}