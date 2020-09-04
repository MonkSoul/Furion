using Fur.FriendlyException;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// EF Core 内置异常
    /// </summary>
    [ErrorCodeType]
    public enum EFCoreErrorCodes
    {
        /// <summary>
        /// 未找到数据
        /// </summary>
        [ErrorCodeItemMetadata("Sequence contains no elements", ErrorCode = "EFCore.DataNotFound")]
        DataNotFound
    }
}