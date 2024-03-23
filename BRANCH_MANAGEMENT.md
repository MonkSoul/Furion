# 分支管理

## 1. 工作流概述

`Furion` 项目采用了 [GitFlow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) 工作流进行分支管理，这是一种专门为大型项目设计的长期支持性分支模型。其核心理念是分离出主干分支（`main` 或 `master`）、开发分支 (`develop`)、特性分支 (`feature/*`)、发布分支 (`release/*`) 和热修复分支 (`hotfix/*`)。

## 2. 主要分支类型

### 2.1 主干分支（Main Branch）

- `main`: 代表生产环境下的稳定版本。只有在发布新版本时才从发布分支合并到主干分支。

### 2.2 开发分支（Develop Branch）

- `develop`: 是日常开发的主要分支，包含所有即将发布的功能。所有特性分支完成后均应合并至开发分支。

### 2.3 特性分支（Feature Branches）

- `feature/*`: 当开发新的功能或改进时，应从开发分支 (`develop`) 上创建特性分支。完成后，通过 Pull Request 合并回 `develop` 分支。

### 2.4 发布分支（Release Branches）

- `release/*`: 当准备发布新版本时，从 `develop` 分支创建发布分支，用于做最终的测试和文档修订。确认无误后，合并到 `main` 和 `develop` 分支。

### 2.5 热修复分支（Hotfix Branches）

- `hotfix/*`: 如果生产环境中发现严重问题，需要立即修复，则从 `main` 分支创建热修复分支。修复完成后，同时合并回 `main` 和 `develop` 分支。

## 3. 分支操作指南

1. **新建特性分支**: `git checkout -b feature/your-feature develop`
2. **完成开发**: 完成开发后，确保本地代码通过所有测试，并将特性分支推送到远程仓库。
3. **发起 Pull Request**: 在 GitHub/Gitee 等平台上对比 `develop` 分支发起 Pull Request，并等待至少一名维护者的审核和合并。
4. **合并分支**: 经过评审和必要的讨论后，由维护者将特性分支合并到 `develop` 分支。

## 4. 注意事项

- 请确保在每个提交中附带清晰、简洁的提交消息，遵循项目的提交消息规范。
- 在合并前确保分支是最新的，避免出现合并冲突。

感谢您的配合，遵循上述分支管理策略将使 `Furion` 项目的开发更加有序、高效！
