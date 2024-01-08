namespace Furion.Application;

/// <summary>
/// 测试异常
/// </summary>
public class TestException : IDynamicApiController
{
    public string GetText(string errorCode, bool hideErrorCode = false)
    {
        return Oops.Text(errorCode, hideErrorCode);
    }
}

[ErrorCodeType]
public enum ErrorCodes
{
    [ErrorCodeItemMetadata("{0} 不能小于 {1}")]
    z1000,

    [ErrorCodeItemMetadata("数据不存在")]
    x1000,
}