// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
            IsRoot = true
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
        // 获取 [DependsOn] 特性
        var dependsOnAttribute = componentType.GetCustomAttribute<DependsOnAttribute>(true);

        // 获取依赖组件列表
        var dependComponents = dependsOnAttribute?.DependComponents?.Distinct()?.ToArray() ?? Array.Empty<Type>();

        // 获取链接组件列表
        var linkComponents = dependsOnAttribute?.LinkComponents?.Distinct()?.ToArray() ?? Array.Empty<Type>();

        // 检查自引用
        if (dependComponents.Contains(componentType) || linkComponents.Contains(componentType))
        {
            throw new InvalidOperationException("A component cannot reference itself.");
        }

        // 找出当前组件的序号
        var index = dependLinkList.IndexOf(componentType);

        // 获取根组件上下文并设置依赖
        var rootComponentContext = componentContextLinkList.First(u => u.IsRoot);

        // 设置当前组件依赖
        var calledContext = index > -1 ? componentContextLinkList[index] : rootComponentContext;
        calledContext.DependComponents = dependComponents;
        calledContext.LinkComponents = linkComponents;

        // 处理链接组件
        if (index == -1)
        {
            index = dependLinkList.Count;

            // 将链接的依赖的组件插入链表指定位置中
            dependLinkList.Add(componentType);

            // 记录组件上下文调用链
            componentContextLinkList.Add(new ComponentContext
            {
                CalledContext = calledContext,
                RootContext = rootComponentContext,
                ComponentType = componentType,
                DependComponents = dependComponents,
                LinkComponents = linkComponents
            });
        }

        // 遍历当前组件依赖的组件集合
        foreach (var dependComponent in dependComponents)
        {
            // 如果还未插入组件链，则插入
            if (!dependLinkList.Contains(dependComponent))
            {
                // 将被依赖的组件插入链表指定位置中
                dependLinkList.Insert(index, dependComponent);

                // 记录组件上下文调用链
                componentContextLinkList.Insert(index, new ComponentContext
                {
                    CalledContext = calledContext,
                    RootContext = rootComponentContext,
                    ComponentType = dependComponent,
                    DependComponents = dependComponents,
                    LinkComponents = linkComponents
                });

                // 递增序号
                index++;
            }
            // 处理组件循环引用情况
            else
            {
                if (dependLinkList.IndexOf(dependComponent) > index)
                {
                    throw new InvalidOperationException("There is a circular reference problem between components.");
                }
            }

            // 进行下一层依赖递归链查找
            CreateDependLinkList(dependComponent, ref dependLinkList, ref componentContextLinkList);
        }

        if (linkComponents == null || linkComponents.Length == 0) return;

        // 遍历链接组件
        foreach (var linkComponent in linkComponents)
        {
            // 不能链接根节点
            if (linkComponent == rootComponentContext.ComponentType)
            {
                throw new InvalidOperationException("There is a circular reference problem between components.");
            }

            CreateDependLinkList(linkComponent, ref dependLinkList, ref componentContextLinkList);
        }
    }
}