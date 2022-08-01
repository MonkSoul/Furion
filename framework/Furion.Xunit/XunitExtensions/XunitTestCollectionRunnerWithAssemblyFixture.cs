// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 单元测试多个测试实例调用器
/// </summary>
public class XunitTestCollectionRunnerWithAssemblyFixture : XunitTestCollectionRunner
{
    private readonly Dictionary<Type, object> assemblyFixtureMappings;
    private readonly IMessageSink diagnosticMessageSink;

    /// <summary>
    /// 创建服务作用域
    /// </summary>
    private IServiceScope serviceScope;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assemblyFixtureMappings"></param>
    /// <param name="testCollection"></param>
    /// <param name="testCases"></param>
    /// <param name="diagnosticMessageSink"></param>
    /// <param name="messageBus"></param>
    /// <param name="testCaseOrderer"></param>
    /// <param name="aggregator"></param>
    /// <param name="cancellationTokenSource"></param>
    public XunitTestCollectionRunnerWithAssemblyFixture(Dictionary<Type, object> assemblyFixtureMappings,
                                                        ITestCollection testCollection,
                                                        IEnumerable<IXunitTestCase> testCases,
                                                        IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        ITestCaseOrderer testCaseOrderer,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource)
        : base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
    {
        this.assemblyFixtureMappings = assemblyFixtureMappings;
        this.diagnosticMessageSink = diagnosticMessageSink;
    }

    /// <summary>
    /// 单元测试实例测试时触发
    /// </summary>
    /// <param name="testClass"></param>
    /// <param name="class"></param>
    /// <param name="testCases"></param>
    /// <returns></returns>
    protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
    {
        // 定义单元测试实例所有构造函数参数实例集合
        var combinedFixtures = new Dictionary<Type, object>(assemblyFixtureMappings);
        foreach (var kvp in CollectionFixtureMappings)
        {
            combinedFixtures[kvp.Key] = kvp.Value;
        }

        // 获取测试实例构造函数
        var constructors = @class.Type.GetConstructors();

        // 不允许多个构造函数
        if (constructors.Length > 1) throw new InvalidProgramException("More than one constructor declaration found.");

        // 如果声明了构造函数
        if (constructors.Length > 0)
        {
            // 获取构造函数参数
            var parameters = constructors[0]
                .GetParameters()
                .Where(u => !u.ParameterType.Assembly.GetName().Name.StartsWith("xunit."));

            // 创建服务作用域
            serviceScope = App.RootServices.CreateScope();

            // 循环所有接口参数并进行服务解析
            foreach (var parameter in parameters)
            {
                combinedFixtures.TryAdd(parameter.ParameterType, serviceScope.ServiceProvider.GetService(parameter.ParameterType));
            }
        }

        // 创建单元测试实例
        return new XunitTestClassRunner(testClass, @class, testCases, diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource, combinedFixtures).RunAsync();
    }

    /// <summary>
    /// 单元测试实例销毁时触发
    /// </summary>
    /// <returns></returns>
    protected override Task BeforeTestCollectionFinishedAsync()
    {
        serviceScope?.Dispose();
        return base.BeforeTestCollectionFinishedAsync();
    }
}