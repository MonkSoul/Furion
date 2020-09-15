# 定义参数
Param(
    [string[]] $Tables
)

# 获取程序包设置的默认项目
$DefaultProject=Project;

# 获取程序员包设置的默认项目名
$ProjectName=$DefaultProject.ProjectName;

# 判断是否等于 Fur.Database.Migrations
if($ProjectName -ne "Fur.Core"){
    Write-Warning "请将默认项目设置为：Fur.Core";
}

# 执行 Scaffold-DbContext 命令

Scaffold-DbContext Name=DbConnectionString Microsoft.EntityFrameworkCore.SqlServer -Namespace "Fur.Core" -OutputDir "Models" -NoOnConfiguring -DataAnnotations
