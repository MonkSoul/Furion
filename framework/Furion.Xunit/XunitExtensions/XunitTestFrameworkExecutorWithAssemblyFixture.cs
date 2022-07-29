// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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