# 定义参数
Param(
    # 需要生成的表，不填则生成所有表
    [string[]] $Tables,
    # 数据库上下文名
    [string]$Context,
    # 数据库连接字符串名
    [string]$Name,
    # 要保存的目录
    [string]$SaveFolder,
    # 数据库提供器
    [string]$DbProvider,
    # Web 项目
    [string]$EntryProject,
    # 实体项目
    [string]$EntityProject
)

# 获取当前目录
$pwd = pwd;
$rootPath = $pwd.Path;

# 初始化默认值
if ($EntryProject -eq $null -or $EntryProject -eq ""){
    $EntryProject = "Fur.Web.Entry";
}

if ($EntityProject -eq $null -or $EntityProject -eq ""){
    $EntityProject = "Fur.Core";
}

if ($DbProvider -eq $null -or $DbProvider -eq ""){
    $DbProvider = "Microsoft.EntityFrameworkCore.SqlServer";
}

if ($Context -eq $null -or $Context -eq ""){
    $Context = "FurDbContext";
}

if ($Name -eq $null -or $Name -eq ""){
    $Name = "DbConnectionString";
}

if ($SaveFolder -eq $null -or $SaveFolder -eq ""){
    $SaveFolder = "$rootPath\$EntityProject\Entities";
}

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

Write-Warning "Fur Tools v1.0.0 启动中......";
Write-Warning "Fur Tools v1.0.0 启动成功！";

# 获取程序包设置的默认项目
$DefaultProject = Project;

# 获取程序员包设置的默认项目名
$ProjectName = $DefaultProject.ProjectName;

# 判断是否等于 Fur.Database.Migrations
if ($ProjectName -ne "Fur.Core"){
    Write-Warning "请将默认项目设置为：Fur.Core";
}

# 定义临时目录
$TempFolder = "$rootPath\$EntityProject\TempEntities";

Write-Warning "Fur Tools v1.0.0 请选择操作类型：[G] 界面操作，否则命令行操作";
$options = Read-Host '您的选择是';

# 选择 GUI 操作
if($options -eq "G")
{
    # -----------------------------------------------------------------------------
    # 构建 Winform GUI 客户端 [开始]
    # -----------------------------------------------------------------------------

    # 加载数据库表
    function loadDbTable(){
        # 获取选中的数据库连接字符串
        $connStr = $comboBox.SelectedItem;
        if ($connStr -eq $null -or $connStr -eq ""){
            [System.Windows.Forms.MessageBox]::Show("请选择数据库连接字符串后再操作");
            return;
        }

        # 打开数据库读取所有数据库表和视图

        # 创建一个数据库连接对象
        $conn = New-Object System.Data.SqlClient.SQLConnection;

        # 设置连接字符串
        $conn.ConnectionString = $connStr;

        # 打开数据库连接
        $conn.Open();

        # 创建 CMD执行命令
        $cmd = New-Object System.Data.SqlClient.SqlCommand("SELECT i.name+'.'+h.name FROM sys.objects h left join sys.schemas i on h.schema_id=i.SCHEMA_ID WHERE h.type IN('U','V') ORDER BY h.type,i.name,h.name", $conn);

        # 创建一个dataset
        $ds = New-Object System.Data.DataSet;

        # 创建一个适配器
        $da = New-Object System.Data.SqlClient.SqlDataAdapter($cmd);

        # 填充数据
        [void]$da.fill($ds)

        # 关闭数据库连接
        $conn.Close();
    
        $rowCount = $ds.Tables[0].Rows.Count;

        # 填充 Listbox
        for($i = 0;$i -le $rowCount; $i++)
        {
            $rows = $ds.Tables[0].Rows[$i];
            if($rows -ne $null)
            {
                [void] $listBox.Items.Add($rows[0]);
            }
        }
    }

    # 添加 Winform 应用程序
    Add-Type -AssemblyName System.Windows.Forms;
    Add-Type -AssemblyName System.Drawing;

    # 创建一个 Winform 窗口
    $mainForm = New-Object System.Windows.Forms.Form;
    $mainForm.Text = "Fur Tools Generate v1.0.0";
    $mainForm.Size = New-Object System.Drawing.Size(800,600);
    $mainForm.StartPosition = "CenterScreen";

    # 创建组面板
    $baseSetting = New-Object System.Windows.Forms.GroupBox;
    $baseSetting.SuspendLayout();
    $baseSetting.Location = New-Object System.Drawing.Point(15, 15);
    $baseSetting.Size = New-Object System.Drawing.Size(760, 75);
    $baseSetting.Text = "基础设置";
    $baseSetting.TabIndex = 10;
    $baseSetting.TabStop = $false;
    $baseSetting.ResumeLayout($false);
    $baseSetting.PerformLayout();
    $mainForm.Controls.Add($baseSetting);

    # 构建数据库连接字符串提示
    $label = New-Object System.Windows.Forms.Label;
    $label.Location = New-Object System.Drawing.Point(15,35);
    $label.AutoSize = $true;
    $label.Size = New-Object System.Drawing.Size(280,20);
    $label.Text = '选择数据库连接字符串：';
    $label.TabIndex = 9;
    $baseSetting.Controls.Add($label);

    # 连接字典
    $connDic = New-Object -TypeName 'System.Collections.Generic.Dictionary[System.String, System.String]';

    # 构建数据库连接字符串下拉
    $comboBox = New-Object System.Windows.Forms.ComboBox;
    $comboBox.Location = New-Object System.Drawing.Point(200,30);
    $comboBox.Size = New-Object System.Drawing.Size(370,20);
    $comboBox.TabIndex = 9;
    $comboBox.DropDownStyle = [System.Windows.Forms.ComboBoxStyle]::DropDownList;
    # 绑定按钮事件
    $comboBoxClickEventHandler = [System.EventHandler] {
        $connStr = $comboBox.SelectedItem;
        if ($connStr -eq $null -or $connStr -eq ""){
            $btnGenerate.Enabled =$false;
        }
        else{
            $btnGenerate.Enabled =$true;
            $Name = $connDic[$connStr];
        }
    }
    $comboBox.Add_SelectedIndexChanged($comboBoxClickEventHandler);
    $baseSetting.Controls.Add($comboBox);

    # 读取 Web 入口的 appsetting.json 配置的链接字符串
    # -----------------------------------------------------------------------------
    # [开始] 
    # appsetting.json 内容
    $appsetting = Get-Content "$rootPath\.\$EntryProject\appsettings.json" -raw;

    # 获取 appsetting.json 定义的节点
    $connectionDefine = [regex]::Matches($appsetting, '"ConnectionStrings"\s*.\s+\{(?<define>[\s\S]*?)\}');
    if($connectionDefine.Count -eq 0)
    {
        Write-Warning "Fur Tools v1.0.0 未找到 appsetting.json 中定义的数据库连接字符串！";
        Write-Warning "Fur Tools v1.0.0 程序终止！";
        return;
    }

    # 获取连接字符串所有定义
    $connectionDefineContent = $connectionDefine[0].Groups.Value[1];

    # 解析出每一个链接字符串
    $connections = [regex]::Matches($connectionDefineContent, '"(.*?)"\s*.\s*"(?<connectionStr>.*?)"');

    # 生成下拉
    for ($i = 0; $i -le $connections.Count - 1; $i++){
       $key = $connections[$i].Groups.Value[1];
       $value = $connections[$i].Groups.Value[2];
       $result = $comboBox.Items.Add($value);
       $connDic.Add($value,$key);
    }
    # [结束] 
    # -----------------------------------------------------------------------------

    # 构建加载数据库表按钮
    $btnLoad = New-Object System.Windows.Forms.Button;
    $btnLoad.Location = New-Object System.Drawing.Point(595,30);
    $btnLoad.Size = New-Object System.Drawing.Size(150, 25);
    $btnLoad.TabIndex = 9;
    $btnLoad.Text = "加载数据库表和视图";
    # 绑定按钮事件
    $btnLoadClickEventHandler = [System.EventHandler] {
        loadDbTable;
    }
    $btnLoad.Add_Click($btnLoadClickEventHandler);
    $baseSetting.Controls.Add($btnLoad);

    # 创建表和视图面板
    $tableSetting = New-Object System.Windows.Forms.GroupBox;
    $tableSetting.SuspendLayout();
    $tableSetting.Location = New-Object System.Drawing.Point(15, 110);
    $tableSetting.Size = New-Object System.Drawing.Size(760, 390);
    $tableSetting.Text = "数据库表和视图";
    $tableSetting.TabIndex = 10;
    $tableSetting.TabStop = $false;
    $tableSetting.ResumeLayout($false);
    $tableSetting.PerformLayout();
    $mainForm.Controls.Add($tableSetting);

    # 创建表和视图容器
    $listBox = New-Object System.Windows.Forms.Listbox;
    $listBox.BackColor = [System.Drawing.SystemColors]::Window;
    $listBox.FormattingEnabled = $true;
    $listBox.ItemHeight = 20;
    $listBox.TabIndex = 9;
    $listBox.Location = New-Object System.Drawing.Point(15,35);
    $listBox.Size = New-Object System.Drawing.Size(730,340);
    $listBox.SelectionMode = "MultiExtended";
    $tableSetting.Controls.Add($listBox);

    # 创建立即生成按钮和取消生成按钮
    $btnGenerate = New-Object System.Windows.Forms.Button;
    $btnGenerate.Location = New-Object System.Drawing.Point(530,520);
    $btnGenerate.Size = New-Object System.Drawing.Size(100, 25);
    $btnGenerate.TabIndex = 8;
    $btnGenerate.Text = "立即生成";
    $btnGenerate.Enabled =$false;
    $btnGenerate.BackColor = [System.Drawing.SystemColors]::ControlLight
    $btnGenerate.DialogResult = [System.Windows.Forms.DialogResult]::OK;
    $mainForm.AcceptButton = $btnGenerate;
    $mainForm.Controls.Add($btnGenerate);

    $btnCancel = New-Object System.Windows.Forms.Button;
    $btnCancel.Location = New-Object System.Drawing.Point(650,520);
    $btnCancel.Size = New-Object System.Drawing.Size(100, 25);
    $btnCancel.TabIndex = 8;
    $btnCancel.Text = "取消生成";
    $btnCancel.DialogResult = [System.Windows.Forms.DialogResult]::Cancel;
    $mainForm.CancelButton = $btnCancel;
    $mainForm.Controls.Add($btnCancel);

    # 显示窗口
    $mainForm.Topmost = $true;
    $dialogResult = $mainForm.ShowDialog();

    # 判断是否选择了立即生成
    if ($dialogResult -eq [System.Windows.Forms.DialogResult]::OK){
        # 设置选择的表
        $Tables = $listBox.SelectedItems;
        $connKey = $comboBox.SelectedItem;

        # 选择保存目录
        $app = New-Object -com Shell.Application;
        $selectFolder = $app.BrowseForFolder(0, "选择 $EntityProject 项目层目录", 0, "$rootPath\$EntityProject");

        # 赋值给保存文件夹
        $SaveFolder = $selectFolder.Self.Path;
        $Name = $connDic[$connKey];

        if($SaveFolder -eq $null -and $SaveFolder -eq "")
        {
            Write-Warning "Fur Tools v1.0.0 用户取消操作，程序终止！";
            return;
        }
    }
    else{
        Write-Warning "Fur Tools v1.0.0 用户取消操作，程序终止！";
        return;
    }

    # -----------------------------------------------------------------------------
    # 构建 Winform GUI 客户端的 [结束]
    # -----------------------------------------------------------------------------
}

# 执行 Scaffold-DbContext 命令
if ($Tables.Count -eq 0){
    Scaffold-DbContext Name=$Name $DbProvider -Context $Context -Namespace $EntityProject -OutputDir $TempFolder -NoOnConfiguring -DataAnnotations -NoPluralize -Force;
}
else
{
    Scaffold-DbContext Name=$Name $DbProvider -Context $Context -Tables $Tables -Namespace "Fur.Core" -OutputDir $TempFolder -NoOnConfiguring -DataAnnotations -NoPluralize -Force;
}

# 获取 DbContext 生成的配置内容
$dbContextContent = Get-Content "$TempFolder\$Context.cs" -raw;
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
// 以下代码由 Fur Tools v1.0.0 生成                                          
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

#递归获取 生成的所有临时实体文件
$files = Get-ChildItem $TempFolder -Include *.cs -recurse
for ($i = 0; $i -le $files.Count - 1; $i++){
    # 文件名
    $fileName = $files[$i].BaseName;
    # 文件路径
    $filePath = $files[$i].FullName;

    if ($fileName -eq $Context){
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
    if ($dic.ContainsKey($fileName)){
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

# 移动文件
    Move-Item $filePath "$SaveFolder\$fileName.cs" -force
 }

# 删除临时数据库上下文
Remove-Item "$TempFolder\$Context.cs";

# 删除临时实体文件夹
Remove-Item $TempFolder -force;

Write-Warning "全部生成成功。";