# 定义参数
Param(
    [string[]] $Tables,
    [string]$Context,
    [string]$Name
)

# 输出信息
$copyright = @"
// -----------------------------------------------------------------------------
//   ______            _______          _     
//  |  ____|          |__   __|        | |    
//  | |__ _   _ _ __     | | ___   ___ | |___ 
//  |  __| | | | '__|    | |/ _ \ / _ \| / __|
//  | |  | |_| | |       | | (_) | (_) | \__ \
//  |_|   \__,_|_|       |_|\___/ \___/|_|___/
//                                            
// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------
"@;

$copyright;

Write-Warning "Fur Tools Cli v1.0.0 For SqlServer 启动中......";
sleep 2;
Write-Warning "Fur Tools Cli v1.0.0 For SqlServer 启动成功！开始执行代码生成......";
sleep 2;

if($Context -eq $null -or $Context -eq ""){
    $Context = "FurDbContext";
}

if($Name -eq $null -or $Name -eq ""){
    $Name = "DbConnectionString";
}


# 获取当前目录
$pwd = pwd;
$rootPath = $pwd.Path;

# 获取程序包设置的默认项目
$DefaultProject = Project;

# 获取程序员包设置的默认项目名
$ProjectName = $DefaultProject.ProjectName;

# 判断是否等于 Fur.Database.Migrations
if($ProjectName -ne "Fur.Core"){
    Write-Warning "请将默认项目设置为：Fur.Core";
}

# 定义实体保存目录
$SaveFolder = "Entities";

# 执行 Scaffold-DbContext 命令
if($Tables.Count -eq 0){
    Scaffold-DbContext Name=$Name Microsoft.EntityFrameworkCore.SqlServer -Context $Context -Namespace "Fur.Core" -OutputDir $SaveFolder -NoOnConfiguring -DataAnnotations -NoPluralize -Force;
}
else{
    Scaffold-DbContext Name=$Name Microsoft.EntityFrameworkCore.SqlServer -Context $Context -Tables $Tables -Namespace "Fur.Core" -OutputDir $SaveFolder -NoOnConfiguring -DataAnnotations -NoPluralize -Force;
}

# 定义模型完整目录
$modelPath = "$rootPath\Fur.Core\$SaveFolder";

# 获取 DbContext 生成的配置内容
$dbContextContent = Get-Content "$modelPath\$Context.cs" -raw;
$entityConfigures = [regex]::Matches($dbContextContent, "modelBuilder.Entity\<(?<table>\w+)\>\(entity\s=\>\n*[\s\S]*?\{(?<content>[\s\S]*?)\}\);");

# 定义字典集合
$dic = New-Object -TypeName 'System.Collections.Generic.Dictionary[System.String, System.String]';

# 将配置保存到字典中
for ($i = 0; $i -le $entityConfigures.Count - 1; $i++){
    $groups = $entityConfigures[$i].Groups;
    $tableName = $groups.Value[1];
    $configure = $groups.Value[2].Replace("entity.", "entityBuilder.");

    $dic.Add($tableName, $configure);
}

# 定义实体文件头模板
$fileHeader = @"
// -----------------------------------------------------------------------------
// 以下代码由 Fur Tools Cli v1.0.0 生成                                          
// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

"@;

# 定义实体配置模板

$entityConfigure = @"

        public void Configure(EntityTypeBuilder<#Table#> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {#Code#
        }

"@

# 类正则表达式
$classRegex = "public\s+partial\s+class\s+(?<table>\w+)";

# 获取类属性正则表达式
$propRegex = "public\s+partial\s+class\s+(?<table>\w+)\n*[\s\S]*?\{(?<content>[\s\S]*)\}\n*[\s\S]*\}";

#递归获取 Fur.Core 所有 .cs 文件
$files = Get-ChildItem $modelPath -Include *.cs -recurse
for ($i = 0; $i -le $files.Count - 1; $i++){
    # 文件名
    $fileName = $files[$i].BaseName;
    # 文件路径
    $filePath = $files[$i].FullName;

    if($fileName -eq $Context){
        continue;
    }

    # 输出
    Write-Warning "正在生成 $fileName 实体代码......";

    # 读取生成模型内容
    $entityContent = Get-Content $filePath -raw;

    # 获取类属性定义
    $propsContent = [regex]::Match($entityContent, $propRegex).Groups.Value[2];

    $extents = " : IEntity";
    $newPropsContent = $propsContent;
    # 判断模型配置中是否包含配置
    if($dic.ContainsKey($fileName)){
        $extents += ", IEntityTypeBuilder<$fileName>";

        # 添加实体配置内容
        $newPropsContent = $propsContent + ($entityConfigure.Replace("#Table#",$fileName).Replace("#Code#",$dic[$fileName]));
    }

    # 生成继承关系和文件头
    $finalClass = $fileHeader + [regex]::Replace($entityContent, $propRegex, @"
public partial class $fileName$extents
    {
$newPropsContent
    }
}
"@);
    $finalClass;
    $finalClass | Set-Content $filePath;
    Write-Warning "成功生成 $fileName 实体代码";
}

# 删除数据库上下文
Remove-Item "$modelPath\$Context.cs";
Write-Warning "全部生成成功。";