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

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    public interface IScopedDependency : IScoped
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    public interface IScopedDependency<TService1> : IScoped
        where TService1 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2> : IScoped
        where TService1 : class
        where TService2 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    /// <typeparam name="TService4">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3, TService4> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    /// <typeparam name="TService4">服务类型</typeparam>
    /// <typeparam name="TService5">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3, TService4, TService5> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    /// <typeparam name="TService4">服务类型</typeparam>
    /// <typeparam name="TService5">服务类型</typeparam>
    /// <typeparam name="TService6">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3, TService4, TService5, TService6> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    /// <typeparam name="TService4">服务类型</typeparam>
    /// <typeparam name="TService5">服务类型</typeparam>
    /// <typeparam name="TService6">服务类型</typeparam>
    /// <typeparam name="TService7">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3, TService4, TService5, TService6, TService7> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖
    /// </summary>
    /// <typeparam name="TService1">服务类型</typeparam>
    /// <typeparam name="TService2">服务类型</typeparam>
    /// <typeparam name="TService3">服务类型</typeparam>
    /// <typeparam name="TService4">服务类型</typeparam>
    /// <typeparam name="TService5">服务类型</typeparam>
    /// <typeparam name="TService6">服务类型</typeparam>
    /// <typeparam name="TService7">服务类型</typeparam>
    /// <typeparam name="TService8">服务类型</typeparam>
    public interface IScopedDependency<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8> : IScoped
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
    {
    }

    /// <summary>
    /// 作用域服务注册依赖（禁止外部继承）
    /// </summary>
    public interface IScoped : IDependency
    {
    }
}