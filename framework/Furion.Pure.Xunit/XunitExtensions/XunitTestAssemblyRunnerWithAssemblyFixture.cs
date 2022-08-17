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

using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 单元测试程序集调用器
/// </summary>
public class XunitTestAssemblyRunnerWithAssemblyFixture : XunitTestAssemblyRunner
{
    private readonly Dictionary<Type, object> assemblyFixtureMappings = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="testAssembly"></param>
    /// <param name="testCases"></param>
    /// <param name="diagnosticMessageSink"></param>
    /// <param name="executionMessageSink"></param>
    /// <param name="executionOptions"></param>
    public XunitTestAssemblyRunnerWithAssemblyFixture(ITestAssembly testAssembly,
                                                      IEnumerable<IXunitTestCase> testCases,
                                                      IMessageSink diagnosticMessageSink,
                                                      IMessageSink executionMessageSink,
                                                      ITestFrameworkExecutionOptions executionOptions)
        : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
    {
    }

    /// <summary>
    /// 单元测试程序集调用时触发
    /// </summary>
    /// <returns></returns>
    protected async override Task AfterTestAssemblyStartingAsync()
    {
        // 让测试程序集回归初始状态
        await base.AfterTestAssemblyStartingAsync();

        // 查找所有程序集中是否有类型定义了 [AssemblyFixture] 特性，如果有，则实例化并注入到所有单元测试实例中
        Aggregator.Run(() =>
        {
            var fixturesAttrs = ((IReflectionAssemblyInfo)TestAssembly.Assembly).Assembly
                                                                                .GetCustomAttributes(typeof(AssemblyFixtureAttribute), false)
                                                                                .Cast<AssemblyFixtureAttribute>()
                                                                                .ToList();

            // 创建 [AssemblyFixture] 配置的类型实例并注入到所有程序集单元测试实例中
            foreach (var fixtureAttr in fixturesAttrs)
            {
                assemblyFixtureMappings[fixtureAttr.FixtureType] = Activator.CreateInstance(fixtureAttr.FixtureType);
            }
        });
    }

    /// <summary>
    /// 单元测试程序集销毁时触发
    /// </summary>
    /// <returns></returns>
    protected override Task BeforeTestAssemblyFinishedAsync()
    {
        // Make sure we clean up everybody who is disposable, and use Aggregator.Run to isolate Dispose failures
        foreach (var disposable in assemblyFixtureMappings.Values.OfType<IDisposable>())
            Aggregator.Run(disposable.Dispose);

        return base.BeforeTestAssemblyFinishedAsync();
    }

    /// <summary>
    /// 执行多个测试实例
    /// </summary>
    /// <param name="messageBus"></param>
    /// <param name="testCollection"></param>
    /// <param name="testCases"></param>
    /// <param name="cancellationTokenSource"></param>
    /// <returns></returns>
    protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus,
                                                               ITestCollection testCollection,
                                                               IEnumerable<IXunitTestCase> testCases,
                                                               CancellationTokenSource cancellationTokenSource)
    {
        return new XunitTestCollectionRunnerWithAssemblyFixture(assemblyFixtureMappings, testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
    }
}