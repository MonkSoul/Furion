### 使用手册

[视频教程](https://www.bilibili.com/video/BV1EW4y1C71D)

1. 在源码目录下（`src`）创建 `api-services` 文件夹，并将 [https://editor.swagger.io](https://editor.swagger.io) 生成的 `typescript-axios` 代码（`.ts` 后缀文件）放入其中（**注意 `package.json` 文件中 `axios` 的版本，必须和项目保持一致，这里使用的是 `v0.21.1` 版本**）。

![](./swagger-editor.png)

2. 拷贝 `Furion` 源码下的 `clients/axios_vue_react/axios-utils.ts`文件放置到 `api-services` 同级目录下，同时修改 `serveConfig` 中的配置地址为对应的服务器地址，同时**安装 `"axios": "^0.21.1"` 的包到 `package.json` 中**

3. 基本使用

- `Promise` 方式：

```ts
getAPI(SystemAPI)
  .apiGetXXXX()
  .then((res) => {
    var data = res.data.data!;
  })
  .catch((err) => {
    console.log(err);
  })
  .finally(() => {
    console.log("api request completed.");
  });
```

- `async/await` 方式：

```ts
const [err, res] = await feature(getAPI(SystemAPI).apiGetXXX());

if (err) {
  console.log(err);
} else {
  var data = res.data.data!;
}

console.log("api request completed.");
```

---

如果服务器有 API 发生改变，重新生成代码并替换 `api-services` 目录所有代码即可（**建议先删除里面所有文件后再粘贴**）

### 常见问题

在 `Vue3` 项目中启用 `TypeScript` 和 `ESlint` 后报错，只需要修改相关的 `tsconfig.json` 文件添加以下配置即可：

```json
"compilerOptions": {
    "importsNotUsedAsValues": "remove",
    "preserveValueImports": false
  }
```

在 `Angular` 项目中，可能会出现 `override` 错误问题，只需要编辑 `api-services/base.ts` 文件并添加 `override` 关键字即可，如：

> 推荐使用 [Angular 版本](../angular)

```ts
export class RequiredError extends Error {
  override name: "RequiredError" = "RequiredError";
}
```

`Swagger` 多分组处理

如果使用了`Swagger` 多分组的功能，可以升级 `Furion` 框架至 `v3.3.4` 版本，之后启用 `EnableAllGroups` 功能，如：

```json
{
  "SpecificationDocumentSettings": {
    "EnableAllGroups": true
  }
}
```

这样就会自动将所有分组的内容合并到 `All Groups` 中。
