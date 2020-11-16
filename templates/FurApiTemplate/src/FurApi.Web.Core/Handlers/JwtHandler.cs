using Fur.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FurApi.Web.Core
{
    public class JwtHandler : AppAuthorizeHandler
    {
        public override bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            var isValid = context.ValidateJwtBearer(httpContext, out _);
            if (!isValid) return false;

            // 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false

            return true;
        }
    }
}