using Furion.DependencyInjection;
using System.ComponentModel;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// Body 内容选项
    /// </summary>
    [SkipScan]
    public enum BodyContentTypeOptions
    {
        /// <summary>
        /// none
        /// </summary>
        [Description("none")]
        StringContent,

        /// <summary>
        /// application/json;text/json;application/*+json
        /// </summary>
        [Description("application/json;text/json;application/*+json")]
        JsonStringContent,

        /// <summary>
        /// application/xml;text/xml;application/*+xml
        /// </summary>
        [Description("application/xml;text/xml;application/*+xml")]
        XmlStringContent,

        /// <summary>
        /// multipart/form-data
        /// </summary>
        [Description("multipart/form-data")]
        MultipartFormDataContent,

        /// <summary>
        /// x-www-form-urlencoded
        /// </summary>
        [Description("x-www-form-urlencoded")]
        FormUrlEncodedContent
    }
}