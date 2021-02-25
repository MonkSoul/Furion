using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Furion.RemoteRequest.Extensions;
using System.Net;
using System.Threading.Tasks;
using Furion.Utilities;

namespace UnitTestFurion.RemoteRequest.Extensions
{
    [TestClass]
    public class RemoteRequestStringExtensionsTest : BaseServerTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetAsAsync()
        {
            var response = await "http://localhost:5000/api/remote-api-service-test".GetAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //https://gitee.com/monksoul/Furion/issues/I38Q7X
        //{"ip":"112.64.53.113","port":4275,"expire_time":"2021-02-23 17:56:28"}
        //System.Text.Json.JsonException: The JSON value could not be converted to System.DateTime. Path: $.expire_time | LineNumber: 0 | BytePositionInLine: 69. ---> System.FormatException: The JSON value is not in a supported DateTime format.
        [TestMethod]
        public async Task GetAsAsyncGeneric()
        {
            await Assert.ThrowsExceptionAsync<System.Text.Json.JsonException>(async () =>
            {
                //"expire_time":"2021-02-23 17:56:28" 使用默认反序列化配置会异常
                ResponseViewModel result = await "http://localhost:5000/api/remote-api-service-test/generic".GetAsAsync<ResponseViewModel>();
            });
            //获取Furion统一使用的Options
            var defaultOptions = JsonSerializerUtility.GetDefaultJsonSerializerOptions();
            //自定义序列化
            var customDateTimeConverter = new DateTimeConverter();
            defaultOptions.Converters.Add(customDateTimeConverter);
            ResponseViewModel result = await "http://localhost:5000/api/remote-api-service-test/generic".GetAsAsync<ResponseViewModel>(deserResultOptions: defaultOptions);
            Assert.AreEqual("112.64.53.113", result.ip);
            Assert.AreEqual(4275, result.port);
            Assert.AreEqual("2021-02-23 17:56:28", result.expire_time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        public override DateTime Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }
        public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

}
