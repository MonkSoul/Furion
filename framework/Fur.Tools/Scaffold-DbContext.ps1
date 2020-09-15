# 定义参数
Param(
    [string[]] $Tables,
    [string]$Context
)

if($Context -eq $null -or $Context -eq ""){
    $Context = "FurDbContext";
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
$SaveFolder = "Models";

# 执行 Scaffold-DbContext 命令
#Scaffold-DbContext Name=DbConnectionString Microsoft.EntityFrameworkCore.SqlServer -Context $Context -Namespace "Fur.Core" -OutputDir $SaveFolder -NoOnConfiguring -DataAnnotations -NoPluralize;

# 定义模型完整目录
$modelPath = "$rootPath\Fur.Core\$SaveFolder";

# 获取 DbContext 生成的配置内容
$dbContextContent = Get-Content "$modelPath\$Context.cs";
$entityConfigures = [regex]::Matches($dbContextContent, "modelBuilder.Entity\<(?<table>\w+)\>\(entity\s=\>\n+[\s\S]*?\{(?<config>[\s\S]*?)\}\);");
echo $entityConfigures;

#获取 Fur.Core 所有文件
# $files=Get-ChildItem $modelPath;

# $files;