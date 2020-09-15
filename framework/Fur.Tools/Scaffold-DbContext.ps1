# 定义参数
Param(
    [string[]] $Tables
)

# 获取程序包设置的默认项目
$DefaultProject=Project;

# 获取程序员包设置的默认项目名
$ProjectName=$DefaultProject.ProjectName;

# 判断是否等于 Fur.Database.Migrations
if($ProjectName -ne "Fur.Database.Migrations"){
    Write-Warning "请将默认项目设置为：Fur.Database.Migrations";
}