## v1.16.0 （未发布）

- **新特性**

  - [新增] `IDGenerator` 雪花 ID 算法，感谢 [idgenerator](https://gitee.com/yitter/idgenerator) 作者提交 PR [#PR204](https://gitee.com/dotnetchina/Furion/pulls/204) [#I3B60S](https://gitee.com/dotnetchina/Furion/issues/I3B60S)
  - [新增] `DbContext` 刷新多租户缓存拓展方法 [#I39N5U](https://gitee.com/dotnetchina/Furion/issues/I39N5U)
  - [新增] 自定义配置单个控制器名称规范，如小写路由 [#I3A5XL](https://gitee.com/dotnetchina/Furion/issues/I3A5XL)

- **突破性变化**

  - [升级] .NET 5 SDK 至 5.0.4 版本 [#I3ASTL](https://gitee.com/dotnetchina/Furion/issues/I3ASTL)
  - [优化] 插件式开发热插拔功能，实现动态加载卸载 [#PR200](https://gitee.com/dotnetchina/Furion/pulls/200), 感谢 [@SamWangCoder](https://gitee.com/samwangcoder)
  - [重构] `JSON` 序列化功能，提供统一的抽象接口，方便自由替换 `JSON` 库 [#I39GT9](https://gitee.com/dotnetchina/Furion/issues/I39GT9)
  - [重构] 验证失败返回消息模型及规范化接口验证参数 [#I3AFQW](https://gitee.com/dotnetchina/Furion/issues/I3AFQW)
  - [移除] 移除 `JsonSerializerUtility` 静态类及移除属性大写序列化拓展配置 [#I3AFRJ](https://gitee.com/dotnetchina/Furion/issues/I3AFRJ)

- **问题修复**

  - [修复] `MVC` 模式下不支持验证自定义验证逻辑 [#I39LM5](https://gitee.com/dotnetchina/Furion/issues/I39LM5)
  - [修复] 验证数值类型正则表达式不支持负数 bug [#I39YUV](https://gitee.com/dotnetchina/Furion/issues/I39YUV)
  - [修复] 框架启动时无法加载未被引用的程序集 bug [#I3A3Z4](https://gitee.com/dotnetchina/Furion/issues/I3A3Z4)
  - [修复] `EFCoreRepository.IsAttached()` 方法判断错误 bug [#I3A824](https://gitee.com/dotnetchina/Furion/issues/I3A824)
  - [修复] `动态API` 驼峰显示配置无效 bug [#I3AF32](https://gitee.com/dotnetchina/Furion/issues/I3AF32)
  - [修复] `cli.ps1` 不支持新版本 `EFCore` bug [#I3APO9](https://gitee.com/dotnetchina/Furion/issues/I3APO9)
  - [修复] `EFCore` 实体配置 `[Table]` 特性无效 bug [#I3BAYH](https://gitee.com/dotnetchina/Furion/issues/I3BAYH)

- **其他更改**

  - [优化] 优化 `MessageCenter` 性能问题 [#I39PRR](https://gitee.com/dotnetchina/Furion/issues/I39PRR)

- **文档变化**

  - [新增] `Docker` 环境下自动化部署 [#PR209](https://gitee.com/dotnetchina/Furion/pulls/209)
  - [新增] `JSON` 序列化 文档 [#I3B6D8](https://gitee.com/dotnetchina/Furion/issues/I3B6D8)
  - [更新] 跨域、安全授权、即时通信文档

- **问答答疑**

  - [答疑] `Furion.Extras.DatabaseAccessor.SqlSugar` 配置多个数据库打印 SQL 语句问题 [#I39PDC](https://gitee.com/dotnetchina/Furion/issues/I39PDC)
  - [答疑] `ORACLE` 数据库多租户模式下返回值为指定类型时系统卡死 [#I39RNH](https://gitee.com/dotnetchina/Furion/issues/I39RNH)
  - [答疑] 假删除指向异常 [#I39XZA](https://gitee.com/dotnetchina/Furion/issues/I39XZA)
  - [答疑] `Furion` 多语言配置节是放在 `AppSettings` 里面还是外面呢？ [#I3A4SB](https://gitee.com/dotnetchina/Furion/issues/I3A4SB)
  - [答疑] 没找到数据库上下文 [#I3A5HS](https://gitee.com/dotnetchina/Furion/issues/I3A5HS)
  - [答疑] 有 `QQ` 交流群吗？ [#I3AAM7](https://gitee.com/dotnetchina/Furion/issues/I3AAM7)
  - [答疑] `Vue3` 环境下配置 `SignalR` 跨域出错 [#I3ALQ7](https://gitee.com/dotnetchina/Furion/issues/I3ALQ7)
  - [答疑] 设置 `Swagger` 参数非必填 [#I3AT02](https://gitee.com/dotnetchina/Furion/issues/I3AT02)
  - [答疑] EFCore 调用 Insert 时报 `Unknown column 'Discriminator' in 'field list'` 异常 [#I3B2LC](https://gitee.com/dotnetchina/Furion/issues/I3B2LC)
  - [答疑] 逆向 `mysql` 数据库时 `cli` 出现错误 [#I3B64F](https://gitee.com/dotnetchina/Furion/issues/I3B64F)
  - [答疑] Sql 高级代理使用过程中 DateTime 类型的参数序列化失败 [#I3AZXK](https://gitee.com/dotnetchina/Furion/issues/I3AZXK)
  - [答疑] 使用 Mysql 执行 Add-Migration 报错 [#I3B8EW](https://gitee.com/dotnetchina/Furion/issues/I3B8EW)
  - [答疑] Saas 多租户模式-独立 Database 模式下无法获取 Tenant, 导致无法自动切换的问题[#I3AVXU](https://gitee.com/dotnetchina/Furion/issues/I3AVXU)
  - [答疑] 如何自定义 WebAPI 统一结果模型 [#I3BBYW](https://gitee.com/dotnetchina/Furion/issues/I3BBYW) [#I3BBYV](https://gitee.com/dotnetchina/Furion/issues/I3BBYV)

- **不做实现**

  - [废弃] 建议 `EFCore` 可配置外键关系导航问题 [#I3994X](https://gitee.com/dotnetchina/Furion/issues/I3994X)
  - [废弃] 建议将 `EFCore` 剥离出来，作为插件的形式提供。这样可以选择自己喜欢的 `ORM` [#I3ABNX](https://gitee.com/dotnetchina/Furion/issues/I3ABNX)
  - [废弃] 事件总线能否提供返回值 [#I3AWL6](https://gitee.com/dotnetchina/Furion/issues/I3AWL6)
  - [废弃] Sql 模板能仿照 Mybatis 一样加各种标签吗？[#I3ASRS](https://gitee.com/dotnetchina/Furion/issues/I3ASRS)
  - [废弃] EFCore 更新或排除更新指定列支持传入 DTO 模型 [#I3AS5K](https://gitee.com/dotnetchina/Furion/issues/I3AS5K)

---

## v1.15.0 （当前版本）

- **新特性**

  - [新增] 跳过特定实体数据库操作监听特性 [#I386LB](https://gitee.com/dotnetchina/Furion/issues/I386LB)
  - [新增] `IEntityChangedListener` 增加对 `OldEntity` 的支持 [#I385X2](https://gitee.com/dotnetchina/Furion/issues/I385X2)
  - [新增] 实时通信自动配置集线器拓展及特性 [#I387QX](https://gitee.com/dotnetchina/Furion/issues/I387QX)
  - [新增] `Mapster` 拓展支持 `IMapper` 依赖注入方式 [#I38C7C](https://gitee.com/dotnetchina/Furion/issues/I38C7C)
  - [新增] `[AppDbContext]` 特性默认构造函数 [#I38J97](https://gitee.com/dotnetchina/Furion/issues/I38J97)
  - [新增] `UnifyContext.GetExceptionMetadata(context)` 返回错误码支持 [#I38ONX](https://gitee.com/dotnetchina/Furion/issues/I38ONX)

- **突破性变化**

- **问题修复**

  - [修复] 多次循环中调用 `Db.GetNewDbContext()` 还是获取到同一个对象 [#I38NNP](https://gitee.com/dotnetchina/Furion/issues/I38NNP)
  - [修复] `Swagger` 过滤掉 `object ` 类型属性问题 [#I38FHL](https://gitee.com/dotnetchina/Furion/issues/I38FHL)
  - [修复] 同一类不支持多继承 `IEntityChangedListener` 问题 [#I38UQJ](https://gitee.com/dotnetchina/Furion/issues/I38UQJ)
  - [修复] 自定义序列化属性名称导致验证失败属性不匹配问题 [#I38W8Z](https://gitee.com/dotnetchina/Furion/issues/I38W8Z)

- **其他更改**

  - [优化] 代码不规范命名导致开发者阅读代码时产生歧义

- **文档变化**

  - [新增] `FluentValidation` 集成文档 [#I38IOT](https://gitee.com/dotnetchina/Furion/issues/I38IOT)

- **问答答疑**

  - [答疑] `Furion` 框架版本向下兼容问题 [#I38WMZ](https://gitee.com/dotnetchina/Furion/issues/I38WMZ)

- **不做实现**

  - [废弃] 建议 `SqlSugar` 添加动态切换数据库功能 [#I38G4M](https://gitee.com/dotnetchina/Furion/issues/I38G4M)
  - [废弃] 建议 `MessageCenter` 采用 `Channel` 实现 [#I38BP8](https://gitee.com/dotnetchina/Furion/issues/I38BP8)
  - [废弃] 建议接口文档整合 Knife4jUI 或 Redoc [#I38S70](https://gitee.com/dotnetchina/Furion/issues/I38S70)

---

## v1.14.0（已发布）

- **新特性**

  - [新增] `EFCore` 5.0 的 `Oracle` 数据库支持 [#I37Z8E](https://gitee.com/dotnetchina/Furion/issues/I37Z8E)
  - [新增] 控制是否在开发环境下显示数据库连接信息 [#I37YQ2](https://gitee.com/dotnetchina/Furion/issues/I37YQ2)
  - [新增] `[NonUnify]` 支持在类中贴此特性 [#I359Q6](https://gitee.com/dotnetchina/Furion/issues/I359Q6)
  - [新增] `网络请求` 字符串 `HttpClient` 拦截器 [#I35F3E](https://gitee.com/dotnetchina/Furion/issues/I35F3E)
  - [新增] `HttpContext` 及 `HttpRequest` 获取远程地址拓展 [#I3688Z](https://gitee.com/dotnetchina/Furion/issues/I3688Z)
  - [新增] `services.AddMvcFilter<>` 添加 `Mvc` 过滤器拓展 [#I368BH](https://gitee.com/dotnetchina/Furion/issues/I368BH)

- **突破性变化**

  - [升级] 框架依赖的 .NET 5 SDK 至最新版 5.0.3 [#I37YQQ](https://gitee.com/dotnetchina/Furion/issues/I37YQQ)
  - [升级] `Swashbuckle.AspNetCore` 组件包到 `6.0.x` 版本 [#I37EZK](https://gitee.com/dotnetchina/Furion/issues/I37EZK)
  - [移除] `Furion` 框架 `JWT` 拓展类，只在 `Furion.Extras.Authentication.JwtBearer` 中保留 [#I35D59](https://gitee.com/dotnetchina/Furion/issues/I35D59)

- **问题修复**

  - [修复] 传入错误 `JWT Token` 字符串导致自动刷新 `Token` 出现字符串边界值异常 bug [#I34ZE5](https://gitee.com/dotnetchina/Furion/issues/I34ZE5)
  - [修复] 瞬时作用域数据库上下文也会自动加入工作单元导致写日志时连锁异常 bug [#I37WTV](https://gitee.com/dotnetchina/Furion/issues/I37WTV)

- **其他更改**

  - [优化] 获取系统环境参数的性能 [#I36SR5](https://gitee.com/dotnetchina/Furion/issues/I36SR5)
  - [优化] `Furion` 底层添加 `Mvc` 过滤器代码 [#I36SKA](https://gitee.com/dotnetchina/Furion/issues/I36SKA)
  - [优化] 添加默认 `Json` 序列化时间默认时间格式 [#I36SL0](https://gitee.com/dotnetchina/Furion/issues/I36SL0)
  - [升级] 升级 `SqlSugar` 拓展包到 `5.0.2.6` 版本 [#I36SIG](https://gitee.com/dotnetchina/Furion/issues/I36SIG)

- **文档变化**

  - [新增] 数据库入门文档 [#I37Z8S](https://gitee.com/dotnetchina/Furion/issues/I37Z8S)
  - [新增] 更新日志文档 [#I36PI0](https://gitee.com/dotnetchina/Furion/issues/I36PI0)
  - [新增] 请求审计日志、执行 `Sql` 更新日志文档 [#I36PIK](https://gitee.com/dotnetchina/Furion/issues/I36PIK)
  - [新增] 前端使用 `axios` 跨域配置文档 [#I36PIT](https://gitee.com/dotnetchina/Furion/issues/I36PIT)
  - [新增] `App` 静态类获取应用、环境更多信息数据 [#I36SOV](https://gitee.com/dotnetchina/Furion/issues/I36SOV)
  - [新增] 英文版 `README.md` 介绍 [#I37QHP](https://gitee.com/dotnetchina/Furion/issues/I37QHP)

- **问答答疑**

- **不做实现**

  - [废弃] 多语言资源文件自动创建 [#I35AA4](https://gitee.com/dotnetchina/Furion/issues/I35AA4)
  - [废弃] 建议多语言加上维吾尔语支持 [#I37X1L](https://gitee.com/dotnetchina/Furion/issues/I37X1L)

---

## v1.13.0（已发布）

- **新特性**

  - [新增] 多语言功能及拓展 [#I2DOCL](https://gitee.com/dotnetchina/Furion/issues/I2DOCL)
  - [新增] 事件总线功能及消息中心 [#I23BKN](https://gitee.com/dotnetchina/Furion/issues/I23BKN)
  - [新增] `Swagger` 分组显示隐藏配置 [#I2AHH8](https://gitee.com/dotnetchina/Furion/issues/I2AHH8)
  - [新增] `Furion.Extras.Logging.Serilog` 拓展插件 [#I2AAN8](https://gitee.com/dotnetchina/Furion/issues/I2AAN8)
  - [新增] `cli.ps` 支持 `-Namespace` 命名空间指定 [#I2A175](https://gitee.com/dotnetchina/Furion/issues/I2A175)
  - [新增] `Swagger` 规范化化文档授权失效后自动取消授权锁 [#I2AIWC](https://gitee.com/dotnetchina/Furion/issues/I2AIWC)
  - [新增] `Request.Body` 支持重复读功能，主要解决微信 SDK 问题 [#I2AMG0](https://gitee.com/dotnetchina/Furion/issues/I2AMG0)
  - [新增] 网络请求功能及文档 [#I2APGJ](https://gitee.com/dotnetchina/Furion/issues/I2APGJ)
  - [新增] `SqlSugar` 拓展包支持打印 `sql` 到 `MiniProfiler` 中 [#I2ASLS](https://gitee.com/dotnetchina/Furion/issues/I2ASLS)
  - [新增] `Furion.Extras.DatabaseAccesssor.Dapper` 拓展插件 [#I2ASYA](https://gitee.com/dotnetchina/Furion/issues/I2ASYA)
  - [新增] `Furion.Extras.DatabaseAccessor.PetaPoco` 拓展插件 [#I2AUGA](https://gitee.com/dotnetchina/Furion/issues/I2AUGA)
  - [新增] 网络请求字符串拓展方法 [#I2CPQ0](https://gitee.com/dotnetchina/Furion/issues/I2CPQ0)
  - [新增] `SqlSugar` 拓展新增 `PagedList` 拓展 [#I2CW99](https://gitee.com/dotnetchina/Furion/issues/I2CW99)
  - [新增] 远程请求支持参数特性验证 [#I2CX5L](https://gitee.com/dotnetchina/Furion/issues/I2CX5L)
  - [新增] `App.User` 获取当前授权用户信息便捷方法 [#I2CZLO](https://gitee.com/dotnetchina/Furion/issues/I2CZLO)
  - [新增] 规范化文档可配置功能，支持 `appsettings.json` 配置 [#I2D1K9](https://gitee.com/dotnetchina/Furion/issues/I2D1K9)
  - [新增] 远程请求拦截器添加方法和方法参数 [#I2D2CM](https://gitee.com/dotnetchina/Furion/issues/I2D2CM)
  - [新增] 远程请求出错返回默认值支持 [#I2D44M](https://gitee.com/dotnetchina/Furion/issues/I2D44M)
  - [新增] 远程请求 `body` 参数序列化支持设置 `PropertyNamingPolicy` [#I2D685](https://gitee.com/dotnetchina/Furion/issues/I2D685)
  - [新增] 远程服务接口客户端配置 [#I2D7PS](https://gitee.com/dotnetchina/Furion/issues/I2D7PS)
  - [新增] `AddInject` 和 `UseInject` 允许自定义 `SecurityDefinitions` 和 `SwaggerUI` [#I2DIMG](https://gitee.com/dotnetchina/Furion/issues/I2DIMG)
  - [新增] `[SecurityDefine]` 默认构造函数 [#I2DNXT](https://gitee.com/dotnetchina/Furion/issues/I2DNXT)
  - [新增] `AspectDispatchProxy` 动态代理类 [#I2DO6I](https://gitee.com/dotnetchina/Furion/issues/I2DO6I)
  - [新增] `[QueryParameters]` 特性，支持一键将 `Action` 参数添加 `[FromQuery]` 特性 [#I2G8TF](https://gitee.com/dotnetchina/Furion/issues/I2G8TF)
  - [新增] 动态日志配置及拓展方法 [#I2GDGD](https://gitee.com/dotnetchina/Furion/issues/I2GDGD)
  - [新增] `WebApi` 请求谓词默认规则配置功能 [#I2M70X](https://gitee.com/dotnetchina/Furion/issues/I2M70X)

- **突破性变化**

  - [升级] `.NET 5` SDK 到 `.NET 5.0.2` 版本 [#I2D0PZ](https://gitee.com/dotnetchina/Furion/issues/I2D0PZ)
  - [调整] 框架内所有拓展类命名空间，全部迁移到 `Furion.模块.Extensions` 下 [#I2AH54](https://gitee.com/dotnetchina/Furion/issues/I2AH54)
  - [调整] `Swagger` 记住授权存储方式，替换 `Session` 存储方式为 `LocalStorage` 方式 [#I2AKUA](https://gitee.com/dotnetchina/Furion/issues/I2AKUA)
  - [调整] `Furion` 框架包描述文件，减少框架体积 [#I2APAU](https://gitee.com/dotnetchina/Furion/issues/I2APAU)
  - [调整] `App.CanBeScanTypes` 为 `App.EffectiveTypes` [#I2B0ZR](https://gitee.com/dotnetchina/Furion/issues/I2B0ZR)
  - [调整] `App.ServiceProvider` 属性并移除 `App.GetDuplicateXXX` 方法 [#I2CYZE](https://gitee.com/dotnetchina/Furion/issues/I2CYZE)
  - [调整] `Db.GetDuplicateDbContext` 为 `Db.GetNewDbContext` [#I2CZ04](https://gitee.com/dotnetchina/Furion/issues/I2CZ04)
  - [调整] `Db.GetSqlDispatchProxy` 为 `Db.GetSqlProxy` [#I2DO9T](https://gitee.com/dotnetchina/Furion/issues/I2DO9T)
  - [重构] `Aop` 服务拦截器，支持异步、同步两种方式 [#I2B9HQ](https://gitee.com/dotnetchina/Furion/issues/I2B9HQ)
  - [重构] 网络请求所有功能 [#I2BMR7](https://gitee.com/dotnetchina/Furion/issues/I2BMR7)

- **问题修复**

  - [修复] `Swagger` 规范化化结果不一致 bug [#I2ACF3](https://gitee.com/dotnetchina/Furion/issues/I2ACF3)
  - [修复] 数据库新增或更新忽略空值操作方法报空异常 [#I2AB6C](https://gitee.com/dotnetchina/Furion/issues/I2AB6C)
  - [修复] `Startup.cs` Aop 全局拦截无效 [#I2A7T2](https://gitee.com/dotnetchina/Furion/issues/I2A7T2)
  - [修复] `Token` 过期后自动刷新 `Token` 无法获取最新的用户信息 bug [#I2AWQI](https://gitee.com/dotnetchina/Furion/issues/I2AWQI)
  - [修复] `[ApiDescriptionSettings(Tag="xx")]` 导致 `swagger.json` 报错 bug [#I2B47R](https://gitee.com/dotnetchina/Furion/issues/I2B47R)
  - [修复] `Mysql` sql 数据库查询结果 `tinyint` 类型转换出错 bug [#I2BEBM](https://gitee.com/dotnetchina/Furion/issues/I2BEBM)
  - [修复] 规范化结果多次包裹类型 bug [#I2BHHZ](https://gitee.com/dotnetchina/Furion/issues/I2BHHZ)
  - [修复] 动态 Api 基元类型数组问题 [#I2BMS5](https://gitee.com/dotnetchina/Furion/issues/I2BMS5)
  - [修复] `sql` 查询枚举类型转换异常 bug [#I2BS2Y](https://gitee.com/dotnetchina/Furion/issues/I2BS2Y)
  - [修复] `string.SqlQuerizeAsync<T1>()` 拓展返回错误 bug [#I2BSTS](https://gitee.com/dotnetchina/Furion/issues/I2BSTS)
  - [修复] 动态 Api 子类重写父类方法并取别名后 `Swagger` 异常 bug [#I2C9VP](https://gitee.com/dotnetchina/Furion/issues/I2C9VP)
  - [修复] 网络请求 `application/json` 序列化大小写问题 [#I2CRJC](https://gitee.com/dotnetchina/Furion/issues/I2CRJC)
  - [修复] 多数据库定位器实体嵌套关联 bug [#I2CVN0](https://gitee.com/dotnetchina/Furion/issues/I2CVN0)
  - [修复] 跨域响应头设置无效 bug [#I2CW5T](https://gitee.com/dotnetchina/Furion/issues/I2CW5T)
  - [修复] 远程网络请求代理打印到 `MiniProfiler` bug [#I2CZBC](https://gitee.com/dotnetchina/Furion/issues/I2CZBC)
  - [修复] 远程请求响应拦截器 bug [#I2D4DG](https://gitee.com/dotnetchina/Furion/issues/I2D4DG)
  - [修复] `SqlSugar` 框架 `AsQueryable()` 一直追加参数 [#I2DH1D](https://gitee.com/dotnetchina/Furion/issues/I2DH1D)
  - [修复] 自动刷新 `Token` 空异常 bug [#I2DO29](https://gitee.com/dotnetchina/Furion/issues/I2DO29)
  - [修复] 生成 `JWT Token` 不传过期时间出现验证 401 bug [#I2DO8L](https://gitee.com/dotnetchina/Furion/issues/I2DO8L)
  - [修复] `AppStartup` 排序无效 [#I2DVD2](https://gitee.com/dotnetchina/Furion/issues/I2DVD2)
  - [修复] 未启用多语言服务时友好异常和验证出现空异常 [#I2ECUJ](https://gitee.com/dotnetchina/Furion/issues/I2ECUJ)
  - [修复] 数据校验字母和数字组合无法匹配 bug [#I2EF2Q](https://gitee.com/dotnetchina/Furion/issues/I2EF2Q)
  - [修复] 数据校验手机或固话无效 bug [#I2M5IZ](https://gitee.com/dotnetchina/Furion/issues/I2M5IZ)
  - [修复] `Dapper` 拓展解析 `SqlConnection` 异常 bug [#I2M5P2](https://gitee.com/dotnetchina/Furion/issues/I2M5P2)
  - [修复] 开启多语言后，`EF` 迁移异常 bug [#I2M7DT](https://gitee.com/dotnetchina/Furion/issues/I2M7DT)
  - [修复] `IEntityTypeBuilder` 不支持多重继承 bug [#I2PAOD](https://gitee.com/dotnetchina/Furion/issues/I2PAOD)
  - [修复] `JwtHandler` 设置自动刷新后，匿名访问无法通过 bug [#I2SDOX](https://gitee.com/dotnetchina/Furion/issues/I2SDOX)
  - [修复] `Dapper` 拓展中 `SqlServer` 数据库获取连接对象类型 bug [#PR159](https://gitee.com/dotnetchina/Furion/pulls/159)

- **其他更改**

  - [优化] 移除 `Sql` 查询结果映射检查 `[NotMapper]` 特性机制 [#I34XD0](https://gitee.com/dotnetchina/Furion/issues/I34XD0)
  - [优化] 依赖注入时排除 `IDynamicApiController` 接口 [#I2ECTG](https://gitee.com/dotnetchina/Furion/issues/I2ECTG)
  - [优化] `MD5` 加密性能 [#PR158](https://gitee.com/dotnetchina/Furion/pulls/158)

- **文档变化**

  - [重构] 文档首页 [#I34XBR](https://gitee.com/dotnetchina/Furion/issues/I34XBR)
  - [新增] 网络请求文档 [#I2APGJ](https://gitee.com/dotnetchina/Furion/issues/I2APGJ)
  - [新增] 多语言文档 [#I2DOCL](https://gitee.com/dotnetchina/Furion/issues/I2DOCL)
  - [新增] 文档全文搜索引擎 [#I34XAW](https://gitee.com/dotnetchina/Furion/issues/I34XAW)
  - [新增] 全局静态类类型 [#I34XB4](https://gitee.com/dotnetchina/Furion/issues/I34XB4)
  - [新增] 框架可配置选项文档 [#I34XB9](https://gitee.com/dotnetchina/Furion/issues/I34XB9)
  - [新增] 事件总线文档 [#I34XBI](https://gitee.com/dotnetchina/Furion/issues/I34XBI)
  - [新增] 数据加解密文档 [#I34XC0](https://gitee.com/dotnetchina/Furion/issues/I34XC0)
  - [新增] 贡献指南文档 [#I34XC8](https://gitee.com/dotnetchina/Furion/issues/I34XC8)
  - [新增] `HttpContext` 及 `文件上传下载` 博客文章 [#I34XCB](https://gitee.com/dotnetchina/Furion/issues/I34XCB)
  - [其他] 文档小调整，小优化

- **问答答疑**

  - [答疑] 跨域设置无效 [#I2ASNJ](https://gitee.com/dotnetchina/Furion/issues/I2ASNJ)
  - [答疑] `MVC` 视图无效，原因是 `.cshtml` 文件没有设置为 `内容` [#I2AXUU](https://gitee.com/dotnetchina/Furion/issues/I2AXUU)
  - [答疑] `Sql` 操作可以实现事务吗？[#I2B0NX](https://gitee.com/dotnetchina/Furion/issues/I2B0NX)
  - [答疑] `IRepository` 操作数据库会打开多次数据库连接 [#I2BB7B](https://gitee.com/dotnetchina/Furion/issues/I2BB7B)
  - [答疑] 如何进入自定义 `AppAuthorizeHandler` 断点 [#I2BGXY](https://gitee.com/dotnetchina/Furion/issues/I2BGXY)
  - [答疑] `SqlSugar` 注入问题 [#I2C2AQ](https://gitee.com/dotnetchina/Furion/issues/I2C2AQ)
  - [答疑] 建议增加 API 签名验证，时效验证 [#I2C6ET](https://gitee.com/dotnetchina/Furion/issues/I2C6ET)
  - [答疑] 多数据库多租户同时使用 `Add-Migration` 报错 [#I2CEHS](https://gitee.com/dotnetchina/Furion/issues/I2CEHS)
  - [答疑] `ISqlSugarRepository` 没有 `Getxxx` 方法 [#I2CJLZ](https://gitee.com/dotnetchina/Furion/issues/I2CJLZ)
  - [答疑] `cli.ps1` 如何将 `sql` 里的表导出成 `model` 类 [#I2CSUL](https://gitee.com/dotnetchina/Furion/issues/I2CSUL)
  - [答疑] 手动修改 `Swagger` 终结点路径无效 [#I2D608](https://gitee.com/dotnetchina/Furion/issues/I2D608)
  - [答疑] `DefaultDbContext` 不能识别 [#I2DCZX](https://gitee.com/dotnetchina/Furion/issues/I2DCZX)
  - [答疑] 各分层项目 `Startup.cs` 支持 `Configuration` [#I2DDUP](https://gitee.com/dotnetchina/Furion/issues/I2DDUP)
  - [答疑] `Aop` 无法拦截，无效 [#I2DEY8](https://gitee.com/dotnetchina/Furion/issues/I2DEY8)
  - [答疑] `mysql` 执行 `Add-Migration` 报错 [#I2DSB8](https://gitee.com/dotnetchina/Furion/issues/I2DSB8)\
  - [答疑] `Entity` 创建时间和是否删除添加默认值 [#I2E04H](https://gitee.com/dotnetchina/Furion/issues/I2E04H)
  - [答疑] `swagger` 中多个 `servers` 设置 [#I2E0IF](https://gitee.com/dotnetchina/Furion/issues/I2E0IF)
  - [答疑] 全局筛选器 没有执行 [#I2E5R4](https://gitee.com/dotnetchina/Furion/issues/I2E5R4)
  - [答疑] 多数据库定位器疑问 [#I2E77T](https://gitee.com/dotnetchina/Furion/issues/I2E77T)
  - [答疑] `cli.ps` 逆向工程 `Mysql` 数据库报错 [#I2E7I5](https://gitee.com/dotnetchina/Furion/issues/I2E7I5)
  - [答疑] `Swagger` 开发环境 `applicationsettings.json` 中文乱码 [#I2EAG1](https://gitee.com/dotnetchina/Furion/issues/I2EAG1)
  - [答疑] 增加指定路径程序集映射 [#I2EEO2](https://gitee.com/dotnetchina/Furion/issues/I2EEO2)
  - [答疑] 动态编译 `cs` 脚本文件 [#I2EH66](https://gitee.com/dotnetchina/Furion/issues/I2EH66)
  - [答疑] 自定义中间件，返回的错误没有规范化结果 [#I2NV8S](https://gitee.com/dotnetchina/Furion/issues/I2NV8S)
  - [答疑] `Swagger` 循环引用设置生成文档层级无效 [#I2PLQQ](https://gitee.com/dotnetchina/Furion/issues/I2PLQQ)
  - [答疑] 配置文件支持 `yaml` 文件吗? [#I2TJ3N](https://gitee.com/dotnetchina/Furion/issues/I2TJ3N)
  - [答疑] 修改数据库未 `mysql` 执行 `Add-Migration` 报错 [#I2VR64](https://gitee.com/dotnetchina/Furion/issues/I2VR64)
  - [答疑] 多数据库使用定位器时报错 [#I2VR8F](https://gitee.com/dotnetchina/Furion/issues/I2VR8F)
  - [答疑] `Migration To Oracle` 异常 [#I2WBYQ](https://gitee.com/dotnetchina/Furion/issues/I2WBYQ)
  - [答疑] 开发时显示 `Swagger`，上线时关闭 `Swagger`，这需要怎么配置 [#I2WOYV](https://gitee.com/dotnetchina/Furion/issues/I2WOYV)
  - [答疑] 兼容 Mvc 复杂验证没有试验成功 [#I2X3GV](https://gitee.com/dotnetchina/Furion/issues/I2X3GV)
  - [答疑] `Aop` 能不能支持无接口的类 [#I2X8AS](https://gitee.com/dotnetchina/Furion/issues/I2X8AS)
  - [答疑] 关于 `JWT Token` 自动刷新问题 [#I2YD4K](https://gitee.com/dotnetchina/Furion/issues/I2YD4K)
  - [答疑] 能否增加一个拓展的 `Entity`，增加一些拓展的属性 [#I2YDKT](https://gitee.com/dotnetchina/Furion/issues/I2YDKT)
  - [答疑] `Furion` 无法还原包，使用`Nuget` 下载和通过最新的的脚手架下载都提示这个问题 [#I30446](https://gitee.com/dotnetchina/Furion/issues/I30446)
  - [答疑] 复杂校验与特性验证不能并行 [#I3046U](https://gitee.com/dotnetchina/Furion/issues/I3046U)

- **不做实现**

  - [废弃] 引入 Webhook 机制 [#I2A3I0](https://gitee.com/dotnetchina/Furion/issues/I2A3I0)
  - [废弃] 增加 blog 存储服务 [#I2AMBP](https://gitee.com/dotnetchina/Furion/issues/I2AMBP)
  - [废弃] Blazor Server 环境下 EF 的 DbContext 多线程问题 [#I2AMD2](https://gitee.com/dotnetchina/Furion/issues/I2AMD2)
  - [废弃] 希望实现工作单元操作接口（IUnitOfWork）[#I2AOR5](https://gitee.com/dotnetchina/Furion/issues/I2AOR5)
  - [废弃] 建议新增 `string.ToPagedListAsync()` 操作 [#I2BZ3Z](https://gitee.com/dotnetchina/Furion/issues/I2BZ3Z)
  - [废弃] 建议新增 `GraphQL`，`OData` 功能 [#I2C9EH](https://gitee.com/dotnetchina/Furion/issues/I2C9EH)
  - [废弃] 迫切期望支持 CAP 分布式事务 [#I2CBRF](https://gitee.com/dotnetchina/Furion/issues/I2CBRF)
  - [废弃] 如何让某个方法或实体变化后，不被 `SavingChangesEvent` 等拦截 [#I2CEM9](https://gitee.com/dotnetchina/Furion/issues/I2CEM9)
  - [废弃] 建议日志封装增加更多拓展，比如：自定义保存日志文件 [#I2CW8V](https://gitee.com/dotnetchina/Furion/issues/I2CW8V)
  - [废弃] 对 `SqlSugar` 的支持，增加 `Nuget` 脚手架 [#I2D91U](https://gitee.com/dotnetchina/Furion/issues/I2D91U)
  - [废弃] 使用 `Middleware` 实现反向代理 [#I2DKM1](https://gitee.com/dotnetchina/Furion/issues/I2DKM1)
  - [废弃] 支持配置多个 `Aop` 功能 [#I2E6Z2](https://gitee.com/dotnetchina/Furion/issues/I2E6Z2)
  - [废弃] 是否计划开发类似网关的功能 [#I2PD1L](https://gitee.com/dotnetchina/Furion/issues/I2PD1L)
  - [废弃] `waring CS8002`: 引用程序集 `Furion` 没有强名称 [#I2WDN2](https://gitee.com/dotnetchina/Furion/issues/I2WDN2)
  - [废弃] Oracle 数据库驱动集成 `Devart.Data.Oracle.Entity.EFCore` [#I2XJIU](https://gitee.com/dotnetchina/Furion/issues/I2XJIU)

---

## v1.7.0（已发布）

- **新特性**

  - [新增] `Furion.Extras.ObjectMapper.Mapster` 拓展包 [#I29LSJ](https://gitee.com/dotnetchina/Furion/issues/I29LSJ)
  - [新增] `Furion.Extras.Logging.Serilog` 拓展包 [#I2AAN8](https://gitee.com/dotnetchina/Furion/issues/I2AAN8)
  - [新增] `Furion.Extras.Web.HttpContext` 拓展包 [#I29LSM](https://gitee.com/dotnetchina/Furion/issues/I29LSM)
  - [新增] 内置 `Token` 刷新机制支持 [#I29K57](https://gitee.com/dotnetchina/Furion/issues/I29K57)
  - [新增] 动态数据库上下文，支持运行时执行 `OnModelCreating` [#I28UDT](https://gitee.com/dotnetchina/Furion/issues/I28UDT)
  - [新增] 支持依赖注入排除指定接口 [#I29693](https://gitee.com/dotnetchina/Furion/issues/I29693)
  - [新增] 规范化结果返回时间戳字段 [#I29697](https://gitee.com/dotnetchina/Furion/issues/I29697)
  - [新增] 基础 `CURD` 父类操作例子 [#I296SR](https://gitee.com/dotnetchina/Furion/issues/I296SR)
  - [新增] `sql.Change("定位器完整类型名称")` 支持 [#I29LAB](https://gitee.com/dotnetchina/Furion/issues/I29LAB)
  - [新增] `UpdateInclude` 和 `UpdateExclude` 忽略空参数支持 [#I29VUG](https://gitee.com/dotnetchina/Furion/issues/I29VUG)
  - [新增] 数据库上下文内置假删除查询过滤器支持 [#I29Y2R](https://gitee.com/dotnetchina/Furion/issues/I29Y2R)
  - [新增] 忽略空值排除默认时间格式 [#I29VUV](https://gitee.com/dotnetchina/Furion/issues/I29VUV)
  - [升级] `MiniProfiler` 组件 [#I297R9](https://gitee.com/dotnetchina/Furion/issues/I297R9)

- **突破性变化**

  - [调整] `AppAuthorizeHandler` 授权管道为异步处理 [#I29MD9](https://gitee.com/dotnetchina/Furion/issues/I29MD9)
  - [调整] `Swagger` 默认启用 `JWT` 授权支持 [#I29LI4](https://gitee.com/dotnetchina/Furion/issues/I29LI4)
  - [调整] `HttpContextUtilities` 名称改为 `HttpContextLocal` [#I29KQE](https://gitee.com/dotnetchina/Furion/issues/I29KQE)
  - [调整] `UnifyResultContext` 名称改为 `UnifyContext` [#I29LLZ](https://gitee.com/dotnetchina/Furion/issues/I29LLZ)
  - [调整] 只有执行迁移命令才扫描种子数据 [#I29E6P](https://gitee.com/dotnetchina/Furion/issues/I29E6P)
  - [调整] 规范化结果 `Successed` 属性名为 `Succeeded` [#I29NMV](https://gitee.com/dotnetchina/Furion/issues/I29NMV)
  - [移除] `Mapster` 对象组件，采用提供拓展方式 [#I29D2M](https://gitee.com/dotnetchina/Furion/issues/I29D2M)
  - [移除] `CacheManager` 拓展类 [#I29LU1](https://gitee.com/dotnetchina/Furion/issues/I29LU1)
  - [重构] `SaveChanges` 拦截器 [#I292LO](https://gitee.com/dotnetchina/Furion/issues/I292LO)

- **问题修复**

  - [修复] 未注册的数据库上下文也被引用全局查询拦截器 bug [#I29ZXJ](https://gitee.com/dotnetchina/Furion/issues/I29ZXJ)
  - [修复] 手动返回 `BadObjectResult` 或 `ValidationProblemDetails` 结果类型时规范化结果失效 bug [#I29ZU9](https://gitee.com/dotnetchina/Furion/issues/I29ZU9)
  - [修复] 动态 WebApi `KeepName`，`KeepVerb`、`SplitCamelCase` 无效 bug [#I29X90](https://gitee.com/dotnetchina/Furion/issues/I29X90)
  - [修复] `Sql代理` 返回 `元组` 类型出错 bug [#I29SMV](https://gitee.com/dotnetchina/Furion/issues/I29SMV)
  - [修复] `401，403` 状态码规范化返回值属性变大写 bug [#I29M8Y](https://gitee.com/dotnetchina/Furion/issues/I29M8Y)
  - [修复] `HttpContext` 空异常 bug [#I29LU4](https://gitee.com/dotnetchina/Furion/issues/I29LU4)
  - [修复] 接口无返回值没有应用规范化结果 bug [#I29GT7](https://gitee.com/dotnetchina/Furion/issues/I29GT7)
  - [修复] 前端 `Less` 配置文件导致主机启动失败 bug [#I29E7P](https://gitee.com/dotnetchina/Furion/issues/I29E7P)
  - [修复] 执行 `sql` 结果转泛型后属性重复赋值 bug [#I29BUO](https://gitee.com/dotnetchina/Furion/issues/I29BUO)
  - [修复] `Swagger` 关闭 `MiniProfiler` 之后 `组中组` 失效 [#I29789](https://gitee.com/dotnetchina/Furion/issues/I29789)
  - [修复] 未启用规范化结果时异常返回 `System.Object` 字符 [#I2969A](https://gitee.com/dotnetchina/Furion/issues/I2969A)
  - [修复] 正数数据验证 0 也验证通过 bug [#I2955T](https://gitee.com/dotnetchina/Furion/issues/I2955T)
  - [修复] 非泛型类集成泛型接口依赖注入 bug [#I294YT](https://gitee.com/dotnetchina/Furion/issues/I294YT)
  - [修复] `Swagger` 不支持 `new` 覆盖父类的 bug [#I28Z1A](https://gitee.com/dotnetchina/Furion/issues/I28Z1A)
  - [修复] `JsonSerializerUtility` 没有公开 bug [#I28WMI](https://gitee.com/dotnetchina/Furion/issues/I28WMI)
  - [修复] `SqlSugar` 拓展查询泛型类型注册异常 bug [#I28VMT](https://gitee.com/dotnetchina/Furion/issues/I28VMT)
  - [修复] `Furion Tools` 不支持生成不同命名空间的实体 bug [#I2A175](https://gitee.com/dotnetchina/Furion/issues/I2A175)
  - [修复] 全局拦截器无效 bug [#I2A7T2](https://gitee.com/dotnetchina/Furion/issues/I2A7T2)
  - [修复] 新增或更新忽略空值空异常 bug [#I2AB6C](https://gitee.com/dotnetchina/Furion/issues/I2AB6C)

- **其他更改**

  - [优化] `Token` 生成加密算法 [#I29KIH](https://gitee.com/dotnetchina/Furion/issues/I29KIH)

- **文档变化**

  - [新增] 日志文档 [#I28Y9D](https://gitee.com/dotnetchina/Furion/issues/I28Y9D)
  - [调整] 数据库上下文、实体拦截器、配置、一分钟入门等等文档

- **问答答疑**

  - [问答] `Swagger` 如何实现授权访问 [#I294F2](https://gitee.com/dotnetchina/Furion/issues/I294F2)
  - [问答] 如何实现多个数据库多对多实体配置 [#I29G6S](https://gitee.com/dotnetchina/Furion/issues/I29G6S)
  - [问答] 动态 WebApi 支持文件上传吗 [#I29R5E](https://gitee.com/dotnetchina/Furion/issues/I29R5E)
  - [疑问] 多个数据库上下文无法生成迁移代码 [#I2A6II](https://gitee.com/dotnetchina/Furion/issues/I2A6II)

- **不做实现**

  - [废弃] 实现 `BuildChange` 传入表名 [#I292SN](https://gitee.com/dotnetchina/Furion/issues/I292SN)
  - [废弃] 执行数据库操作结果不支持属性忽略大小写赋值 [#I29DRQ](https://gitee.com/dotnetchina/Furion/issues/I29DRQ)
  - [废弃] 引入 `Webhook` 机制 [#I2A3I0](https://gitee.com/dotnetchina/Furion/issues/I2A3I0)

---

## v1.4.0（已发布）

- **新特性**

  - [新增] `Furion` 支持二级虚拟目录部署功能 [#I28B77](https://gitee.com/dotnetchina/Furion/issues/I28B77)
  - [新增] `Furion.Template.RazorWithWebApi` 脚手架 [#I28QGI](https://gitee.com/dotnetchina/Furion/issues/I28QGI)
  - [新增] `Furion.Template.BlazorWithWebApi` 脚手架 [#I27Z3O](https://gitee.com/dotnetchina/Furion/issues/I27Z3O)
  - [新增] `EFCore` 时态查询拓展 [#I28AJ](https://gitee.com/dotnetchina/Furion/issues/I28AJ6)
  - [新增] `[AppDbContext(连接字符串，数据库类型)]` 配置支持 [#I28QTB](https://gitee.com/dotnetchina/Furion/issues/I28QTB)
  - [新增] `DateTimeOffset` 转 `DateTime` 拓展方法 [#I27MQA](https://gitee.com/dotnetchina/Furion/issues/I27MQA)
  - [新增] `ValidationTypes` 验证正则表达式智能提示 [#I2801V](https://gitee.com/dotnetchina/Furion/issues/I2801V)
  - [新增] `ValiationTypes.WordWithNumber` 验证 [#I2805](https://gitee.com/dotnetchina/Furion/issues/I2805A)
  - [新增] 获取客户端和服务端 IP 地址 [#I28QV9](https://gitee.com/dotnetchina/Furion/issues/I28QV9)

- **突破性变化**

  - [升级] .NET 5.0 版本至 .NET 5.0.1 版本 [#I28QU](https://gitee.com/dotnetchina/Furion/issues/I28QU1)
  - [重构] 视图引擎功能，优化不规范命名和新增字符串模板编译 [#I28G0S](https://gitee.com/dotnetchina/Furion/issues/I28G0S)
  - [重构] 数据库实体查找算法，并优化性能 [#I28QUQ](https://gitee.com/dotnetchina/Furion/issues/I28QUQ)
  - [优化] 应用启动初始化性能和数据库第一次自动配置 `DbSet` 性能

- **问题修复**

  - [修复] 多数据库上下文配置定位器后实体无法正确生成 bug [#I2888L](https://gitee.com/dotnetchina/Furion/issues/I2888L)
  - [修复] 多租户数据库上下文实体生成 bug [#I2891G](https://gitee.com/dotnetchina/Furion/issues/I2891G)
  - [修复] 对象验证失败提示消息没有应用 `JSON` 大小写配置 bug [#I27UTX](https://gitee.com/dotnetchina/Furion/issues/I27UTX)
  - [修复] 仓储 `Insert` 或 `Update` 方法指定 `ignoreNullValues` 无效 bug [#I27UN6](https://gitee.com/dotnetchina/Furion/issues/I27UN6)
  - [修复] `Controller` 派生类如果贴了 `[Route]` 特性后出现在 `Swagger` 中 bug [#I27TN7](https://gitee.com/dotnetchina/Furion/issues/I27TN7)
  - [修复] `SqlScalar` 执行 `sql` 返回 `Nullable` 类型出现转换失败 bug [#I27S2N](https://gitee.com/dotnetchina/Furion/issues/I27S2N)
  - [修复] `[UnitOfWork]` 特性异常 bug [#I27MLM](https://gitee.com/dotnetchina/Furion/issues/I27MLM)
  - [修复] `sql` 静态执行方式和 `sql` 高级代理无法监听数据库连接状态 bug [#I27M4F](https://gitee.com/dotnetchina/Furion/issues/I27M4F)
  - [修复] 修复更换 Json 序列化库无效 bug，如替换为 `Microsoft.AspNetCore.Mvc.NewtonsoftJson` [#I27M43](https://gitee.com/dotnetchina/Furion/issues/I27M43)
  - [修复] `Furion Tools` 工具生成模型 bug [#I27XI5](https://gitee.com/dotnetchina/Furion/issues/I27XI5)
  - [修复] 软删除没有生效 bug [#I2804I](https://gitee.com/dotnetchina/Furion/issues/I2804I)
  - [修复] `Furion Tools` 识别带多个 `\\` 的连接字符串识别 bug [#I280TS](https://gitee.com/dotnetchina/Furion/issues/I280TS)，[#PR91](https://gitee.com/dotnetchina/Furion/pulls/91)
  - [修复] `Furion Tools` 无法取消生成 bug [#I2816M](https://gitee.com/dotnetchina/Furion/issues/I2816M)
  - [修复] `DateTimeOffset` 转本地时间差 8 小时 bug [#I28BA9](https://gitee.com/dotnetchina/Furion/issues/I28BA9)
  - [修复] 启用 `bundle js&css` 压缩后启动异常 bug [#I28KR](https://gitee.com/dotnetchina/Furion/issues/I28KRP)
  - [修复] `ValidationTypes.Required` 无效 bug [#PR98](https://gitee.com/dotnetchina/Furion/pulls/98)
  - [修复] 规范化结果`OnValidateFailed` 参数名拼写错误 bug [#PR93](https://gitee.com/dotnetchina/Furion/pulls/93)，[#PR92](https://gitee.com/dotnetchina/Furion/pulls/92)
  - [修复] 授权管道验证失败还显示结果 bug [#PR89](https://gitee.com/dotnetchina/Furion/pulls/89)

- **其他更改**

  - [更新] README.md 友情连接地址 [#PR88](https://gitee.com/dotnetchina/Furion/pulls/88)
  - [更新] 模板脚手架源码，添加 `EFCore Tools` 库 [#PR87](https://gitee.com/dotnetchina/Furion/pulls/87)
  - [更新] README.md Nuget 图标 [#PR85](https://gitee.com/dotnetchina/Furion/pulls/85)
  - [废弃] 将 `List<T>` 转 `DateTable` [#PR97](https://gitee.com/dotnetchina/Furion/pulls/97)

- **文档变化**

  - [新增] 视图引擎模板文档 [#I27ZVA](https://gitee.com/dotnetchina/Furion/issues/I27ZVA)
  - [新增] `EFCore` 时态查询文档 [#I28AJ](https://gitee.com/dotnetchina/Furion/issues/I28AJ6), [DOC](https://dotnetchina.gitee.io/furion/docs/dbcontext-hight-query/#91111-%E6%97%B6%E6%80%81%E6%9F%A5%E8%AF%A2)
  - [更新] 仓储文档书写纰漏 bug [#PR90](https://gitee.com/dotnetchina/Furion/pulls/90)
  - [更新] 选项文档错误 bug [#PR86](https://gitee.com/dotnetchina/Furion/pulls/86)
  - [更新] `实体数据监听器` 文档书写错误 bug [#PR83](https://gitee.com/dotnetchina/Furion/pulls/83/files)
  - [更新] 数据库上下文、多数据库、脚手架等文档

- **问答答疑**

  - [建议] 希望 `api` 返回的值自动将 null 转为 '' 或 [] [#I286IJ](https://gitee.com/dotnetchina/Furion/issues/I286IJ)，【已关闭】
  - [建议] 添加网关功能 [#I27TP7](https://gitee.com/dotnetchina/Furion/issues/I27TP7)，【已关闭】
  - [建议] 新增 `SqlQuery<T>` 获取单条记录方法 [#I28M1V](https://gitee.com/dotnetchina/Furion/issues/I28M1V)，【已关闭】
  - [建议] 希望可以提供集成 `Serilog` 例子 [#I282J4](https://gitee.com/dotnetchina/Furion/issues/I282J4)，【已关闭】
  - [疑问] 如何通过特性配置唯一约束 [#I2891L](https://gitee.com/dotnetchina/Furion/issues/I2891L)，【已关闭】
  - [疑问] 怎么读取 `appsettings.json` 数组 [#I27WU](https://gitee.com/dotnetchina/Furion/issues/I27WUR)，【已关闭】
  - [疑问] `IRepository<TEntity>` 出现空异常 [#I281IE](https://gitee.com/dotnetchina/Furion/issues/I281IE)，【已关闭】
  - [疑问] 规范化接口问题问题 [#I28NMZ](https://gitee.com/dotnetchina/Furion/issues/I28NMZ)，【已关闭】
  - [疑问] 统一返回值模型中 OnResponseStatusCodes 未执行 [#I28NNL](https://gitee.com/dotnetchina/Furion/issues/I28NNL)，【已关闭】

---

## v1.2.0（已发布）

- **新特性**

  - [新增] 雪花算法 [#I26OXG](https://gitee.com/dotnetchina/Furion/issues/I26OXG), [#PR78](https://gitee.com/dotnetchina/Furion/pulls/78)
  - [新增] `[AppDbContext]` 配置数据库提供器支持 [#I27G3T](https://gitee.com/dotnetchina/Furion/issues/I27G3T)
  - [新增] 实体表数据更改监听接口 `IEntityDataChangedListener` [#I278DD](https://gitee.com/dotnetchina/Furion/issues/I278DD), [#I278LQ](https://gitee.com/dotnetchina/Furion/issues/I278LQ)
  - [新增] 全局服务接口 AOP 拦截功能 [#I278CP](https://gitee.com/dotnetchina/Furion/issues/I278CP)
  - [新增] 定位器仓储 `IDbRepository<TDbContextLocator>` [#I276Q3](https://gitee.com/dotnetchina/Furion/issues/I276Q3)
  - [新增] 数据库操作 `InsertOrUpdate` 支持排除空字符串功能 [#I272OG](https://gitee.com/dotnetchina/Furion/issues/I272OG)
  - [新增] 数据库操作 `UpdateInclude` 和 `UpdateExclude` 匿名对象支持 [#I271X0](https://gitee.com/dotnetchina/Furion/issues/I271X0)
  - [新增] 数据验证传入空对象跳过验证支持 [#I273R4](https://gitee.com/dotnetchina/Furion/issues/I273R4)
  - [新增] 应用启动时支持排除特定配置文件自动加载 [#I26U0A](https://gitee.com/dotnetchina/Furion/issues/I26U0A)
  - [新增] 单个实体表名前缀支持 [#I26LX0](https://gitee.com/dotnetchina/Furion/issues/I26LX0)
  - [新增] `MySql` 数据库自动配置默认版本号 [#I26XQ6](https://gitee.com/dotnetchina/Furion/issues/I26XQ6)
  - [优化] 授权处理程序代码

- **突破性变化**

  - [新增] 实体表数据更改监听接口 `IEntityDataChangedListener` [#I278DD](https://gitee.com/dotnetchina/Furion/issues/I278DD), [#I278LQ](https://gitee.com/dotnetchina/Furion/issues/I278LQ)
  - [新增] 全局服务接口 AOP 拦截功能 [#I278CP](https://gitee.com/dotnetchina/Furion/issues/I278CP)
  - [新增] 雪花算法 [#I26OXG](https://gitee.com/dotnetchina/Furion/issues/I26OXG), [#PR78](https://gitee.com/dotnetchina/Furion/pulls/78)

- **问题修复**

  - [修复] 视图引擎加载外部程序集出错 bug
  - [修复] 依赖注入代理接口报空对象异常 bug
  - [修复] `EFCore` 取消附加实体出错 bug
  - [修复] 数据库仓储在非 Web 请求下出现空异常 bug
  - [修复] 多个授权策略共存问题出现无效 bug
  - [修复] 友好异常 `Oop.Oh` 不支持普通方法 bug
  - [修复] 获取多租户对象时数据库上下文出现作用域验证失败 bug
  - [修复] 工作单元不支持 `Sql代理` 拦截 bug [#I27GST](https://gitee.com/dotnetchina/Furion/issues/I27GST)

- **其他更改**

  - [关闭] [#I26O1F](https://gitee.com/dotnetchina/Furion/issues/I26O1F), [#I27B2I](https://gitee.com/dotnetchina/Furion/issues/I27B2I), [#I27BJ6](https://gitee.com/dotnetchina/Furion/issues/I27BJ6), [#I27E5Z](https://gitee.com/dotnetchina/Furion/issues/I27E5Z), [#I27EL4](https://gitee.com/dotnetchina/Furion/issues/I27EL4)

- **文档变化**

  - [新增] [实体数据监听器](https://dotnetchina.gitee.io/furion/docs/dbcontext-entitytrigger) 文档
  - [更新] 一分钟入门、应用启动、官方脚手架、数据库操作指南、对象映射、规范化文档、异常处理、鉴权授权文档

---

## v1.1.0（已发布）

- **新特性**

  - [新增] `Db.GetDbContext()` 获取默认数据库上下文方法
  - [新增] `HttpContextUtility.GetCurrentHttpContext()` 获取全局 `HttpContext` 上下文
  - [新增] `App.GetRequiredService<>` 解析服务方法
  - [新增] `object.GetService<>` 对象拓展方法
  - [新增] 策略授权 `PolicyPipeline` 基类方法，支持多重判断授权
  - [新增] `JWTEncryption.ValidateJwtBearerToken` 手动验证静态方法
  - [新增] 全局数据库上下文 `InsertOrUpdateIgnoreNullValues` 和 `EnabledEntityStateTracked` 全局配置
  - [新增] `Swagger Jwt授权` 全局授权参数 [#I26GLR](https://gitee.com/dotnetchina/Furion/issues/I26GLR)
  - [新增] `InsertOrUpdate` 支持自定义判断条件功能 [#I269Q1](https://gitee.com/dotnetchina/Furion/issues/I269Q1)
  - [新增] 字符串字段小写命名支持 [#I2695D](https://gitee.com/dotnetchina/Furion/issues/I2695D)
  - [新增] 字符串文本对比功能 [#I268LE](https://gitee.com/dotnetchina/Furion/issues/I268LE)
  - [新增] 全局异常特性消息功能 [#I2662O](https://gitee.com/dotnetchina/Furion/issues/I2662O)
  - [新增] `Insert` 或 `Update` 数据库忽略空值功能 [#I264Q4](https://gitee.com/dotnetchina/Furion/issues/I264Q4)

- **突破性变化**

  - [调整] `Fur` 项目名为 `Furion`
  - [调整] `Db.GetRequestDbContext<>()` 命名为 `Db.GetDbContext<>()`
  - [调整] `Db.GetDbContext<>()` 命名为 `Db.GetDuplicateDbContext<>()`
  - [重构] `App.GetService<>` 解析服务的底层逻辑，大大提高了解析服务的性能
  - [重构] 授权核心代码，保持和微软一致的授权规范 [#I26DCB](https://gitee.com/dotnetchina/Furion/issues/I26DCB)
  - [移除] `App.GetRequestService<>` 方法
  - [移除] `ValidateJwtBearer` Jwt 授权方法，无需手动判断了

- **问题修复**

  - [修复] Furion 官方脚手架生成后编译异常 bug
  - [修复] `Tenant` 内置属性不是 `virtual` 修饰 bug
  - [修复] `dockerfile` 新命名构建失败 bug
  - [修复] 自定义角色授权和多个授权共存出现 403 bug [#I26H1L](https://gitee.com/dotnetchina/Furion/issues/I26H1L)
  - [修复] `httpContext.GetEndpoint()` 空异常 bug [#PR73](https://gitee.com/dotnetchina/Furion/pulls/73)
  - [修复] `Oops.Oh` 空异常和不支持服务服务抛异常 bug [#I26EFU](https://gitee.com/dotnetchina/Furion/issues/I26EFU)，[#I26GM4](https://gitee.com/dotnetchina/Furion/issues/I26GM4)
  - [修复] `cli.ps` 生成文件编码乱码 bug [#I26DVT](https://gitee.com/dotnetchina/Furion/issues/I26DVT)
  - [修复] `Swagger` 文件上传按钮不显示 [#I26B6U](https://gitee.com/dotnetchina/Furion/issues/I26B6U)
  - [修复] 规范化结果授权状态码序列化大小写不一致问题 [#I26B26](https://gitee.com/dotnetchina/Furion/issues/I26B26)
  - [修复] 未启用规范化结果时中文乱码 bug [#I268T5](https://gitee.com/dotnetchina/Furion/issues/I268T5)
  - [修复] `MySql` 异步异常捕获不到 bug [#I265SO](https://gitee.com/dotnetchina/Furion/issues/I265SO)
  - [修复] `cli.ps1` 提示找不到数据库连接字符串 bug [#I2647U](https://gitee.com/dotnetchina/Furion/issues/I2647U)

- **其他更改**

  - [其他] 代码性能小优化和小调整
  - [关闭] [#I265JV](https://gitee.com/dotnetchina/Furion/issues/I265JV)，[#I26ERA](https://gitee.com/dotnetchina/Furion/issues/I26ERA)， [#I26EVW](https://gitee.com/dotnetchina/Furion/issues/I26EVW)，[#I26GHC](https://gitee.com/dotnetchina/Furion/issues/I26GHC)，[#I26GJ1](https://gitee.com/dotnetchina/Furion/issues/I26GJ1)，[#I26O1F](https://gitee.com/dotnetchina/Furion/issues/I26O1F)

- **文档变化**

  - [更新] 一分钟入门、安全鉴权、数据库等文档

---

## v1.0.3（已发布）

- **新特性**

  - [新增] Mvc 模板脚手架：`Fur.Template.Mvc`
  - [新增] WebApi 模板：`Fur.Template.Api`
  - [新增] Mvc/WebApi 模板：`Fur.Template.App`
  - [新增] Razar Pages 模板：`Fur.Template.Razor`
  - [新增] Blazor 模板：`Fur.Template.Blazor`

- **突破性变化**

  - [调整] `PagedList` 到 `System.Collections.Generic` 命名空间下
  - [优化] 解析服务性能问题，底层代码大量优化

- **问题修复**

  - [修复] `ApiSears.ControllerEnd`不起作用 bug [#I25KH6](https://gitee.com/monksoul/Fur/issues/I25KH6)
  - [修复] `RemoteRequest` 请求完成结果序列化属性大小写问题 [#I25I8R](https://gitee.com/monksoul/Fur/issues/I25I8R)
  - [修复] `HttpContext.GetEndpoinet()` 空异常 bug [#PR73](https://gitee.com/monksoul/Fur/pulls/73)

- **其他更改**

  - 无

- **文档变化**

  - [更新] 入门文档、数据库上下文文档、多数据库操作文档

---

## v1.0.2（已发布）

- **新特性**

  - [新增] `Pomelo.EntityFrameworkCore.MySql` 最新 .NET 5 包配置 [#I24ZQK](https://gitee.com/monksoul/Fur/issues/I24ZQK)
  - [新增] `.AddDateTimeJsonConverter(format)` 时间格式序列化配置
  - [新增] `DateTime` 和 `DateTimeOffset` 类型序列化格式配置 [#I253FI](https://gitee.com/monksoul/Fur/issues/I253FI)

- **突破性变化**

  - [更新] `Mapster` 包至 `7.0.0` 版
  - [调整] `App.Services` 名为 `App.ServiceProvider`
  - [移除] `App.ApplicationServices` 和 `App.GetRequestService<>()`
  - [移除] 非 Web 主机注入拓展

- **问题修复**

  - [修复] `services.AddFriendlyException()` 缺少配置注入 bug
  - [修复] 数据库上下文池被释放和高并发下内存溢出 bug [#I2524K](https://gitee.com/monksoul/Fur/issues/I2524K)，[#I24UMN](https://gitee.com/monksoul/Fur/issues/I24UMN)
  - [修复] `Sql代理` 返回空数据时异常 bug [#I24TCK](https://gitee.com/monksoul/Fur/issues/I24TCK)
  - [修复] 工作单元 `[UnitOfWork]` 多数据库被释放 bug [#I24Q6W](https://gitee.com/monksoul/Fur/issues/I24Q6W)

- **其他更改**

  - [调整] `EntityBase` 和 `Entity` 所有属性为 `vitural` 修饰
  - [优化] `Jwt` 读取和解析性能
  - [优化] 优化代码支持 C# 9.0 最新语法
  - [优化] `MD5` 加密性能 [#PR71](https://gitee.com/monksoul/Fur/pulls/71)
  - [移除] 无用或未使用代码

- **文档**

  - [更新] 数据库上下文、多数据库、一分钟入门文档

---

## v1.0.0（已发布）

- **新特性**

  - [新增] 网络请求 `RemoteRequest` 组件 [#I1YYWD](https://gitee.com/monksoul/Fur/issues/I1YYWD)
  - [新增] `.AddInjectBase()` 注入，只包含基础服务注入
  - [新增] 所有服务都支持 `IServiceCollection` 和 `IMvcBuilder` 注入
  - [新增] 抛异常状态码设置功能 `StatusCode`
  - [新增] `Swagger` 序列化支持 `Pascal` 属性命名方式

- **突破性变化**

  - [更新] **所有的包为 `.NET 5` 正式版**

- **问题修复**

  - [修复] `SqlProxy` 代理异步处理 bug
  - [修复] 数据库类型 `Datetime` 转 `DateTimeOffset` bug
  - [修复] 属性首字母大小写序列化不匹配出现 `null` bug
  - [修复] 对象序列化中文出现乱码 bug
  - [修复] 默认序列化配置无效 bug
  - [修复] 数据库非依赖注入方式提交无效 bug
  - [修复] 应用程序池提交所有 `DbContext` 空异常 bug
  - [修复] `Saas` 多租户 `Tenant` 类型字符串属性在 `MySql` 数据库下出现 `longtext` 类型 bug
  - [修复] `Mvc` 自动验证字符串空值 bug [#I24M2T](https://gitee.com/monksoul/Fur/issues/I24M2T)
  - [修复] 枚举注释被覆盖 bug [#I24N6J](https://gitee.com/monksoul/Fur/issues/I24N6J)
  - [修复] 忽略规范化结果无效 bug [#I24B8P](https://gitee.com/monksoul/Fur/issues/I24B8P)
  - [修复] `Swagger` 默认 `ContentType` 不是 `applicaiton/json` bug [#I24F3U](https://gitee.com/monksoul/Fur/issues/I24F3U)
  - [修复] 内置 `System.Text.Json` 和 `Newtonsoft.Json` 冲突 bug [#I24F3U](https://gitee.com/monksoul/Fur/issues/I24F3U)

- **其他更改**

  - [调整] `Fur` 框架域名为：[https://furos.cn](https://furos.cn)
  - [调整] 仓储 `FromSqlRaw` 和 `FromSqlInterpolated` 接口位置
  - [优化] 数据加解密性能，[#PR70](https://gitee.com/monksoul/Fur/pulls/70)

- **文档**

  - [更新] README.md、框架介绍、数据库上下文、配置选项、多租户、跨域文档
