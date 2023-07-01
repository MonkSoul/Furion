文档基于 [https://dotnet.github.io/docfx/index.html](https://dotnet.github.io/docfx/index.html) 构建。

### 本地运行

```bash
# 安装 docfx
dotnet tool update -g docfx

# 本地浏览
docfx docfx.json --serve
```

### 发布部署

部署 `_site` 目录即可。