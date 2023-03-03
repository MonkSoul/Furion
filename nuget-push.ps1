# 排除 Furion 和 Furion.Pure 发布，原因是这两个包依赖了 Furion.[Pure].Extras.DependencyModel.CodeAnalysis 拓展包
#
# 定义参数
Param(
    # 版本号
    [string] $version,
    # NuGet APIKey
    [string] $apikey
)

if ($version -eq $null -or $version -eq "")
{
    Write-Error "必须指定版本号";
    return;
}

Write-Warning "正在发布 framework 目录 NuGet 包......";

# 查找 .\framework\nupkgs 下所有目录
cd .\framework\nupkgs;
$framework_nupkgs = Get-ChildItem -Filter *.nupkg;

# 遍历所有 *.nupkg 文件
for ($i = 0; $i -le $framework_nupkgs.Length - 1; $i++){
    $item = $framework_nupkgs[$i];

    # 排除 Furion 和 Furion.Pure 发布，原因是这两个包依赖了 Furion.[Pure].Extras.DependencyModel.CodeAnalysis 拓展包
    if ($item.Name -ne "Furion.$version.nupkg" -and $item.Name -ne "Furion.Pure.$version.nupkg" -and $item.Name -ne "Furion.Xunit.$version.nupkg" -and $item.Name -ne "Furion.Pure.Xunit.$version.nupkg")
    {
        $nupkg = $item.FullName;
        $snupkg = $nupkg.Replace(".nupkg", ".snupkg");

        Write-Output "-----------------";
        $nupkg;

        # 发布到 nuget.org 平台
        dotnet nuget push $nupkg --skip-duplicate --api-key $apikey --source https://api.nuget.org/v3/index.json;
        dotnet nuget push $snupkg --skip-duplicate --api-key $apikey --source https://api.nuget.org/v3/index.json;

        Write-Output "-----------------";
    }
}

# 回到项目根目录
cd ../../;

Write-Warning "发布成功";