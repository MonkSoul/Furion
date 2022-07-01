// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Reflection;

namespace Furion.Components;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 创建组件依赖链表
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    internal static List<ComponentContext> CreateDependLinkList(Type componentType, object options = default)
    {
        // 根组件上下文
        var rootComponentContext = new ComponentContext
        {
            ComponentType = componentType,
        };
        rootComponentContext.SetProperty(componentType, options);

        // 初始化组件依赖链
        var dependLinkList = new List<Type> { componentType };
        var componentContextLinkList = new List<ComponentContext>
        {
            rootComponentContext
        };

        // 创建组件依赖链
        CreateDependLinkList(componentType, ref dependLinkList, ref componentContextLinkList);

        return componentContextLinkList;
    }

    /// <summary>
    /// 创建组件依赖链表
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="dependLinkList">依赖链表</param>
    /// <param name="componentContextLinkList">组件上下文链表</param>
    /// <exception cref="InvalidOperationException"></exception>
    internal static void CreateDependLinkList(Type componentType, ref List<Type> dependLinkList, ref List<ComponentContext> componentContextLinkList)
    {
        // 获取组件依赖关系
        var dependComponents = !componentType.IsDefined(typeof(DependsOnAttribute), false)
                        ? Array.Empty<Type>()
                        : componentType.GetCustomAttribute<DependsOnAttribute>().DependComponents.Distinct().ToArray();

        // 出现自引用
        if (dependComponents.Contains(componentType))
        {
            throw new InvalidOperationException("A component cannot reference itself.");
        }

        // 处理无组件依赖情况
        if (dependComponents.Length == 0) return;

        // 找出当前组件的序号
        var index = dependLinkList.IndexOf(componentType);

        // 设置当前组件依赖
        var calledContext = componentContextLinkList[index];
        calledContext.DependComponents = dependComponents;

        // 获取根组件上下文
        var rootComponentContext = componentContextLinkList.Last();

        // 遍历当前组件依赖的组件集合
        foreach (var cmpType in dependComponents)
        {
            // 如果还未插入组件链，则插入
            if (!dependLinkList.Contains(cmpType))
            {
                // 将被依赖的组件插入链表指定位置中
                dependLinkList.Insert(index, cmpType);

                // 记录组件上下文调用链
                componentContextLinkList.Insert(index, new ComponentContext
                {
                    CalledContext = calledContext,
                    RootContext = rootComponentContext,
                    ComponentType = cmpType
                });

                // 递增序号
                index++;
            }
            // 处理组件循环引用情况
            else
            {
                if (dependLinkList.IndexOf(cmpType) > index)
                {
                    throw new InvalidOperationException("There is a circular reference problem between components.");
                }
            }

            // 进行下一层依赖递归链查找
            CreateDependLinkList(cmpType, ref dependLinkList, ref componentContextLinkList);
        }
    }
}