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
using System.Text.Json;

namespace System.Net.Http
{
    [SuppressSniffer]
    public static class HttpExtensions
    {
        public static Task<Stream> ReadAsStreamAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            return content.ReadAsStreamAsync();
        }

        public static Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            return content.ReadAsStringAsync();
        }

        public static Task<byte[]> ReadAsByteArrayAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            return content.ReadAsByteArrayAsync();
        }
    }
}

namespace Microsoft.AspNetCore.Http
{
    [SuppressSniffer]
    public static class HttpExtensions
    {
        public static Task WriteAsJsonAsync<TValue>(this HttpResponse response, TValue value, JsonSerializerOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.ContentType = "application/json; charset=utf-8";
            return JsonSerializer.SerializeAsync(response.Body, value, options, cancellationToken);
        }
    }
}
#endif