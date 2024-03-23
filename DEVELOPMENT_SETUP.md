# 开发环境配置

## 1. 安装 Visual Studio 2022 Preview Enterprise

### 1.1 下载安装

访问 [Visual Studio 官方网站](https://visualstudio.microsoft.com/zh-hans/vs/preview/) 下载 Visual Studio 2022 Preview Enterprise 版本。确保选择适合您操作系统的版本，并确保系统满足最低硬件和软件要求。

### 1.2 安装过程

1. 运行安装程序，选择“自定义”安装选项。
2. 在工作负载选择界面，确保勾选“ASP.NET 和 Web 开发” workload，这将自动包含 .NET 框架的多个版本支持。
3. 在单个组件部分，滚动查找并勾选您需要的 .NET SDK 版本，包括但不限于：
   - .NET 5.x SDK
   - .NET 6.x SDK
   - .NET 7.x SDK
   - .NET 8.x SDK
   - .NET 9.x SDK
4. 确认所有所需组件已勾选后，开始安装过程。

### 1.3 验证安装

安装完成后，打开 Visual Studio 2022 并通过“工具” > “获取工具和功能…”检查已安装的组件，确认 .NET SDK 是否正确安装。

## 2. 安装 CodeMaid 2022

[CodeMaid](https://www.codemaid.net) 是一个优秀的 Visual Studio 扩展，用于提高代码质量和可读性，提供代码清理、格式化等功能。

### 2.1 安装扩展

1. 在 Visual Studio 中，转到“扩展” > “管理扩展”（或快捷键 Ctrl + Shift + X）。
2. 在在线市场搜索栏中输入“CodeMaid”。
3. 找到 CodeMaid 并点击“下载”按钮安装。
4. 安装完成后，重启 Visual Studio 以激活 CodeMaid。

### 2.2 配置 CodeMaid

1. 安装完毕后，可在“工具” > “选项” > “CodeMaid”中配置 CodeMaid 的默认设置。
2. 根据个人喜好和团队规范调整 CodeMaid 的清理规则和代码样式。

## 3. 配置项目

对于新的或现有的 C#/ASP.NET Core 项目，确保在项目属性中设置了正确的 .NET SDK 版本目标框架。

至此，您的开发环境已经准备好支持 C#/ASP.NET Core 开发，并配备了 CodeMaid 代码清理工具，您可以开始愉快地进行项目开发了。

---

注意：由于 .NET SDK 更新频繁，请定期检查并更新至最新版本以获得最佳体验和安全性保障。

---
