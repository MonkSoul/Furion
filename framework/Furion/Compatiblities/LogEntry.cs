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

#if NETCOREAPP3_1
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Logging.Abstractions;

[SuppressSniffer]
public readonly struct LogEntry<TState>
{
    public LogLevel LogLevel { get; }

    public string Category { get; }

    public EventId EventId { get; }

    public TState State { get; }

    public Exception Exception { get; }

    public Func<TState, Exception, string> Formatter { get; }

    public LogEntry(LogLevel logLevel, string category, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        LogLevel = logLevel;
        Category = category;
        EventId = eventId;
        State = state;
        Exception = exception;
        Formatter = formatter;
    }
}
#endif