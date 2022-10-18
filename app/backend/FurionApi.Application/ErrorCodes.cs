using Furion.FriendlyException;

namespace FurionApi.Application;

[ErrorCodeType]
public enum ErrorCodes
{
    [ErrorCodeItemMetadata("账号或密码错误")]
    T1000
}
