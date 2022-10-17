using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FurionApi.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        return Task.FromResult(true);
    }
}