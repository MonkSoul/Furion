// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 规范化结果配置
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class UnifyResultAttribute : ProducesResponseTypeAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="statusCode"></param>
    public UnifyResultAttribute(int statusCode) : base(statusCode)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    public UnifyResultAttribute(Type type) : base(type, StatusCodes.Status200OK)
    {
        WrapType(type);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="statusCode"></param>
    public UnifyResultAttribute(Type type, int statusCode) : base(type, statusCode)
    {
        WrapType(type);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="statusCode"></param>
    /// <param name="method"></param>
    internal UnifyResultAttribute(Type type, int statusCode, MethodInfo method) : base(type, statusCode)
    {
        WrapType(type, method);
    }

    /// <summary>
    /// 包装类型
    /// </summary>
    /// <param name="type"></param>
    /// <param name="method"></param>
    private void WrapType(Type type, MethodInfo method = default)
    {
        if (type != null && UnifyContext.EnabledUnifyHandler)
        {
            var unityMetadata = UnifyContext.GetMethodUnityMetadata(method);

            if (unityMetadata != null && !type.HasImplementedRawGeneric(unityMetadata.ResultType))
            {
                Type = unityMetadata.ResultType.MakeGenericType(type);
            }
            else Type = default;
        }
    }
}