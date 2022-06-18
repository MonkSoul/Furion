# 获取所有 Furion + EFCore 模板目录
$efcore_path = pwd;
$efcore_templates = Get-ChildItem -Directory $efcore_path -Exclude SqlSugarTemplates;

# 获取所有 Furion + SqlSugar 模板目录
cd .\SqlSugarTemplates;
$sqlsugar_path = pwd;
$sqlsugar_templates = Get-ChildItem -Directory $sqlsugar_path;
cd ..;

# 定义生成 Nupkg 包
function generate($path, $templates){
    $dir = $path.Path;
    Write-Warning "正在生成 [$dir] Nupkg 包......";

    # 遍历所有模板
    for ($i = 0; $i -le $templates.Length - 1; $i++){
        $item = $templates[$i];

        # 获取完整路径
        $fullName = $item.FullName;

        Write-Output "-----------------";
        $fullName
        Write-Output "-----------------";

        # 生成 .nupkg 包
        .\nuget.exe pack $fullName;
    }

    Write-Output "生成成功";
}

# 生成 EFCore Nupkg 包
generate -path $efcore_path -templates $efcore_templates

# 生成 SqlSugar Nupkg 包
generate -path $sqlsugar_path -templates $sqlsugar_templates