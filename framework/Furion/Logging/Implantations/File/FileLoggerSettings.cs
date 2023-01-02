// MIT License
//
// Copyright (c) 2020-2023 ��Сɮ, Baiqian Co.,Ltd and Contributors
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

using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// �ļ���־��¼������ѡ��
/// </summary>
[SuppressSniffer]
public sealed class FileLoggerSettings
{
    /// <summary>
    /// ��־�ļ�����·�����ļ������Ƽ� .log ��Ϊ��չ��
    /// </summary>
    public string FileName { get; set; } = null;

    /// <summary>
    /// ׷�ӵ��Ѵ�����־�ļ��򸲸�����
    /// </summary>
    public bool Append { get; set; } = true;

    /// <summary>
    /// ����ÿһ����־�ļ����洢��С��Ĭ��������
    /// </summary>
    /// <remarks>���ָ���˸�ֵ����ô��־�ļ���С�����˸����þͻᴴ������־�ļ����´�������־�ļ����������ļ���+[�������].log</remarks>
    public long FileSizeLimitBytes { get; set; } = 0;

    /// <summary>
    /// ������󴴽�����־�ļ�������Ĭ�������ƣ���� <see cref="FileSizeLimitBytes"/> ʹ��
    /// </summary>
    /// <remarks>���ָ���˸�ֵ����ô������ֵ���������־�ļ��д�ͷд�븲��</remarks>
    public int MaxRollingFiles { get; set; } = 0;

    /// <summary>
    /// �����־��¼����
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;

    /// <summary>
    /// �Ƿ�ʹ�� UTC ʱ�����Ĭ�� false
    /// </summary>
    public bool UseUtcTimestamp { get; set; }

    /// <summary>
    /// ���ڸ�ʽ��
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fffffff zzz dddd";

    /// <summary>
    /// �Ƿ�������־������
    /// </summary>
    public bool IncludeScopes { get; set; } = true;

    /// <summary>
    /// ��ʾ����/���� Id
    /// </summary>
    public bool WithTraceId { get; set; } = false;
}