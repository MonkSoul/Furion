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

using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 单元测试框架执行器
/// </summary>
public class XunitTestFrameworkExecutorWithAssemblyFixture : XunitTestFrameworkExecutor
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <param name="sourceInformationProvider"></param>
    /// <param name="diagnosticMessageSink"></param>
    public XunitTestFrameworkExecutorWithAssemblyFixture(AssemblyName assemblyName
        , ISourceInformationProvider sourceInformationProvider
        , IMessageSink diagnosticMessageSink)
        : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
    {
    }

    /// <summary>
    /// 执行测试案例
    /// </summary>
    /// <param name="testCases"></param>
    /// <param name="executionMessageSink"></param>
    /// <param name="executionOptions"></param>
    protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
    {
        using var assemblyRunner = new XunitTestAssemblyRunnerWithAssemblyFixture(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions);
        await assemblyRunner.RunAsync();
    }
}