# 工作单元与事务

数据的完整性和一致性是应用系统不可或缺的功能。

---

## 事务

### 什么是事务 <Badge text="beta" type="tip"/>

视图指作为单个逻辑工作单元执行的一系列操作，**要么完全地执行，要么完全地不执行**。

简单的说，事务就是并发控制的单位，是用户定义的一个操作序列。 而一个逻辑工作单元要成为事务，就必须满足 `ACID` 属性。

::: details 什么是 ACID
`A`：原子性（Atomicity）：事务中的操作要么都不做，要么就全做。

`C`：一致性（Consistency）：事务执行的结果必须是从数据库从一个一致性状态转换到另一个一致性状态。

`I`：隔离性（Isolation）：一个事务的执行不能被其他事务干扰。

`D`：持久性（Durability）：一个事务一旦提交，它对数据库中数据的改变就应该是永久性的。
:::

### 如何使用 <Badge text="beta" type="error"/>

::: details 查看代码

```cs {1-2,16-17,21-22}
// 开启事务
using (var transaction = _testRepository.Database.BeginTransaction())
{
    try
    {
        _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/dotnet" });
        _testRepository.SaveChanges();

        _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/visualstudio" });
        _testRepository.SaveChanges();

        var blogs = _testRepository.Entity
                .OrderBy(b => b.Url)
                .ToList();

        // 提交事务
        transaction.Commit();
     }
     catch (Exception)
     {
        // 回滚事务
        transaction.RollBack();
     }
}
```

:::

## 工作单元 <Badge text="beta" type="warning"/>

### 什么是工作单元

简单来说，就是为了保证一次完整的功能操作所产生的一些列提交数据的完整性，起着事务的作用。在计算机领域中，工作单元通常用 `UnitOfWork` 名称表示。

通常我们保证用户的 **每一次请求都是处于在一个功能单元中，也就是工作单元**。

### 如何使用

默认情况下，在 Fur 中，工作单元无需我们手动维护，框架会自动保证了每一次请求都是一个工作单元，要么同时成功，要么同时失败。也就是保证了数据的完整性。
