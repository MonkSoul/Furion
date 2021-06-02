using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发器
    /// </summary>
    public interface IUrlRewriter
    {
        /// <summary>
        /// URL转发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rewritePath"></param>
        /// <param name="toHost"></param>
        /// <returns></returns>
        Task RewriteUri(HttpContext context, PathString rewritePath, string toHost);
    }
}