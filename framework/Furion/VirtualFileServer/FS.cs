// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Furion.VirtualFileServer;

/// <summary>
/// 虚拟文件服务静态类
/// </summary>
[SuppressSniffer]
public static class FS
{
    /// <summary>
    /// 获取物理文件提供器
    /// </summary>
    /// <param name="root"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IFileProvider GetPhysicalFileProvider(string root, IServiceProvider serviceProvider = default)
    {
        return GetFileProvider(FileProviderTypes.Physical, root, serviceProvider);
    }

    /// <summary>
    /// 获取嵌入资源文件提供器
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IFileProvider GetEmbeddedFileProvider(Assembly assembly, IServiceProvider serviceProvider = default)
    {
        return GetFileProvider(FileProviderTypes.Embedded, assembly, serviceProvider);
    }

    /// <summary>
    /// 文件提供器
    /// </summary>
    /// <param name="fileProviderTypes"></param>
    /// <param name="args"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IFileProvider GetFileProvider(FileProviderTypes fileProviderTypes, object args, IServiceProvider serviceProvider = default)
    {
        var fileProviderResolve = App.GetService<Func<FileProviderTypes, object, IFileProvider>>(serviceProvider ?? App.RootServices);
        return fileProviderResolve(fileProviderTypes, args);
    }

    /// <summary>
    /// 根据文件名获取文件的 ContentType 或 MIME
    /// </summary>
    /// <param name="fileName">文件名（带拓展）</param>
    /// <param name="contentType">ContentType 或 MIME</param>
    /// <returns></returns>
    public static bool TryGetContentType(string fileName, out string contentType)
    {
        return GetFileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
    }

    /// <summary>
    /// 初始化文件 ContentType 提供器
    /// </summary>
    /// <returns></returns>
    public static FileExtensionContentTypeProvider GetFileExtensionContentTypeProvider()
    {
        var fileExtensionProvider = new FileExtensionContentTypeProvider();
        fileExtensionProvider.Mappings[".iec"] = "application/octet-stream";
        fileExtensionProvider.Mappings[".patch"] = "application/octet-stream";
        fileExtensionProvider.Mappings[".apk"] = "application/vnd.android.package-archive";
        fileExtensionProvider.Mappings[".pem"] = "application/x-x509-user-cert";
        fileExtensionProvider.Mappings[".gzip"] = "application/x-gzip";
        fileExtensionProvider.Mappings[".7zip"] = "application/zip";
        fileExtensionProvider.Mappings[".jpg2"] = "image/jp2";
        fileExtensionProvider.Mappings[".et"] = "application/kset";
        fileExtensionProvider.Mappings[".dps"] = "application/ksdps";
        fileExtensionProvider.Mappings[".cdr"] = "application/x-coreldraw";
        fileExtensionProvider.Mappings[".shtml"] = "text/html";
        fileExtensionProvider.Mappings[".php"] = "application/x-httpd-php";
        fileExtensionProvider.Mappings[".php3"] = "application/x-httpd-php";
        fileExtensionProvider.Mappings[".php4"] = "application/x-httpd-php";
        fileExtensionProvider.Mappings[".phtml"] = "application/x-httpd-php";
        fileExtensionProvider.Mappings[".pcd"] = "image/x-photo-cd";
        fileExtensionProvider.Mappings[".bcmap"] = "application/octet-stream";
        fileExtensionProvider.Mappings[".properties"] = "application/octet-stream";
        fileExtensionProvider.Mappings[".m3u8"] = "application/x-mpegURL";
        return fileExtensionProvider;
    }
}