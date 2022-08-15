// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文关联类型
/// </summary>
internal sealed class DbContextCorrelationType
{
    /// <summary>
    /// 构造函数
    /// </summary>
    internal DbContextCorrelationType()
    {
        EntityTypes = new List<Type>();
        EntityNoKeyTypes = new List<Type>();
        EntityTypeBuilderTypes = new List<Type>();
        EntitySeedDataTypes = new List<Type>();
        EntityChangedTypes = new List<Type>();
        ModelBuilderFilterTypes = new List<Type>();
        EntityMutableTableTypes = new List<Type>();
        ModelBuilderFilterInstances = new List<IPrivateModelBuilderFilter>();
        DbFunctionMethods = new List<MethodInfo>();
    }

    /// <summary>
    /// 关联的数据库上下文
    /// </summary>
    internal Type DbContextLocator { get; set; }

    /// <summary>
    /// 所有关联类型
    /// </summary>
    internal List<Type> Types { get; set; }

    /// <summary>
    /// 实体类型集合
    /// </summary>
    internal List<Type> EntityTypes { get; set; }

    /// <summary>
    /// 无键实体类型集合
    /// </summary>
    internal List<Type> EntityNoKeyTypes { get; set; }

    /// <summary>
    /// 实体构建器类型集合
    /// </summary>
    internal List<Type> EntityTypeBuilderTypes { get; set; }

    /// <summary>
    /// 种子数据类型集合
    /// </summary>
    internal List<Type> EntitySeedDataTypes { get; set; }

    /// <summary>
    /// 实体数据改变类型
    /// </summary>
    internal List<Type> EntityChangedTypes { get; set; }

    /// <summary>
    /// 模型构建筛选器类型集合
    /// </summary>
    internal List<Type> ModelBuilderFilterTypes { get; set; }

    /// <summary>
    /// 可变表实体类型集合
    /// </summary>
    internal List<Type> EntityMutableTableTypes { get; set; }

    /// <summary>
    /// 数据库函数方法集合
    /// </summary>
    internal List<MethodInfo> DbFunctionMethods { get; set; }

    /// <summary>
    /// 模型构建器筛选器实例
    /// </summary>
    internal List<IPrivateModelBuilderFilter> ModelBuilderFilterInstances { get; set; }
}