# 定义参数
Param(
    # NuGet APIKey
    [string] $apikey
)

Write-Warning "正在发布 templates 目录 NuGet 包......";

# 查找所有 *.nupkg 文件
$template_nupkgs = Get-ChildItem -Filter *.nupkg;

# 遍历所有 *.nupkg 文件
for ($i = 0; $i -le $template_nupkgs.Length - 1; $i++){
    $item = $template_nupkgs[$i];

    $nupkg = $item.FullName;

    Write-Output "-----------------";
    $nupkg;

    # 发布到 nuget.org 平台
    dotnet nuget push $nupkg --skip-duplicate --api-key $apikey --source https://api.nuget.org/v3/index.json;

    Write-Output "-----------------";
}

Write-Warning "发布成功";