﻿using Furion.Application.Persons;
using Furion.AspNetCore;
using Furion.ClayObject;
using Furion.DatabaseAccessor.Extensions;
using Furion.Extensions;
using Furion.Logging;
using Furion.Reflection;
using Furion.UnifyResult;
using Furion.ViewEngine;
using Furion.ViewEngine.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace Furion.Application;

/// <summary>
/// 测试模块
/// </summary>
public class TestModuleServices : IDynamicApiController
{
    [HttpPost]
    public IActionResult UploadFileAsync(IFormFile file)
    {
        return new ContentResult() { Content = file.FileName };
    }

    [HttpPost]
    public IActionResult UploadMulitiFileAsync(List<IFormFile> files)
    {
        return new ContentResult() { Content = string.Join(',', files.Select(u => u.FileName)) };
    }

    [NonUnify]
    public IActionResult SpecialApi()
    {
        return new JsonResult(new RESTfulResult<object>
        {
            StatusCode = 200,
            Succeeded = true,
            Data = new
            {
                Name = "Furion"
            },
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        }, new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });
    }

    [UnifySerializerSetting("special")]
    public object SpecialApi2()
    {
        return new
        {
            Name = "Furion"
        };
    }

    [HttpGet, LoggingMonitor]
    public string WithCookies([FromServices] IHttpContextAccessor contextAccessor)
    {
        contextAccessor.HttpContext.Response.Cookies.Append("name", "百小僧");
        contextAccessor.HttpContext.Response.Cookies.Append("age", "30");

        return "Furion";
    }

    public ServiceLifetime? 测试服务生命周期()
    {
        var d = App.GetServiceLifetime(typeof(IConfiguration));
        var c = App.GetServiceLifetime(typeof(IRepository<Person>));

        return d | c;
    }

    [LoggingMonitor(ContractResolver = ContractResolverTypes.CamelCase)]
    public DataTable 测试监听日志属性序列化规则()
    {
        var d = "select * from person".SqlQuery();
        return d;
    }

    public void 测试GUID正则表达式()
    {
        var a = "41E3DAF5-6E37-4BCC-9F8E-0D9521E2AA8D".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;
        var b = "e155518c-ca1b-443c-9be9-fe90fdab7345".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;
        var c = "00000000-0000-0000-0000-000000000000".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;

        var d = true == a && a == b && a == c;
    }

    public void 测试创建新的数据库上下文()
    {
        var dbcontext1 = Db.GetDbContext(typeof(MasterDbContextLocator));
        var dbcontext2 = Db.GetDbContext(typeof(MasterDbContextLocator));

        var c = dbcontext1 == dbcontext2;

        var dbcontext3 = Db.GetNewDbContext(typeof(MasterDbContextLocator));
        var dbcontext4 = Db.GetNewDbContext(typeof(MasterDbContextLocator));

        var d = dbcontext1 != dbcontext3;
        var e = dbcontext2 != dbcontext3;
        var f = dbcontext3 != dbcontext4;

        Task.Run(() =>
        {
            var dbcontext5 = Db.GetNewDbContext(typeof(MasterDbContextLocator));
            var dbcontext6 = Db.GetNewDbContext(typeof(MasterDbContextLocator));

            var f = dbcontext5 != dbcontext6;
        });
    }

    public void 测试JWTSettings()
    {
        var settings = JWTEncryption.GetJWTSettings();
    }

    [HttpPost("api/sse"), AllowAnonymous]
    public async Task CreateSseDemo([FromServices] IHttpContextAccessor accessor)
    {
        // 设置响应头，指定 SSE 的内容类型
        accessor.HttpContext.Response.Headers.Append("Content-Type", "text/event-stream");

        // 写入 SSE 消息到响应流
        for (int i = 0; i < 10; i++)
        {
            var message = $"消息{i}";
            await accessor.HttpContext.Response.WriteAsync(message);
            await accessor.HttpContext.Response.Body.FlushAsync();
            await Console.Out.WriteLineAsync(message);
            Task.Delay(1000).Wait();
        }
        await accessor.HttpContext.Response.CompleteAsync();
    }

    public async Task 测试事务是否回滚和捕获异常()
    {
        try
        {
            await Scoped.CreateUowAsync(async (_, f) =>
            {
                throw Oops.Oh("抛出异常");

                await Task.CompletedTask;
            });
        }
        catch (Exception)
        {
        }
    }

    public async Task<string> 测试模板引擎模式匹配([FromServices] IViewEngine viewEngine)
    {
        var template = """
             @{
                 var ss = new { QueryType = "like"};
                 string result = "";
                 switch (ss)
                 {
                     case {QueryType: "like"}:
                         result = "显示此处内容";
                         break;
                 }
                 @:@result
             }
             """;

        var str = await viewEngine.RunCompileAsync(template);

        return str;
    }

    public TestLong TestLong2(TestLong test)
    {
        return test;
    }

    public dynamic 测试嵌套Clay和序列化()
    {
        dynamic a1 = Clay.Object(new
        {
            Name = "我是第一层"
        });

        dynamic a2 = Clay.Object(new
        {
            Name = "我是第二层"
        });

        dynamic a3 = Clay.Object(new object[] { });

        a3[0] = new
        {
            Name = "明细1"
        };

        a3[1] = new
        {
            Name = "明细2"
        };

        a1.Child = a2;
        a1.Entry = a3;

        var str = a1.ToString();

        foreach (var item in a1)
        {
            var key = item.Key;
            var value = item.Value;

            if (value is Clay clay)
            {
                if (clay.IsObject)
                {
                    a1[key] = "df21";
                }

                if (clay.IsArray)
                {
                    var currentArr = Clay.Object(new dynamic[] { });
                    for (int i = 0; i < value.Length; i++)
                    {
                        var sss = a1[key][i].ToString();
                        var vs = $"我是 {key}{i} {sss}";

                        if (i == 0)
                        {
                            // 这里是成功的
                            currentArr[i] = vs;
                        }
                        else
                        {
                            // 这里是成功的
                            currentArr[i] = Clay.Object(new
                            {
                                Name = vs
                            });
                        }
                    }

                    // 这种写法不行
                    a1[key] = currentArr;
                }
            }
        }

        return a1;
    }

    public dynamic 测试嵌套Clay和序列化2()
    {
        // 这里是另外一个Arr 子集
        dynamic a3 = Clay.Object(new object[] { });

        // 创建粘土
        dynamic a1 = Clay.Object(new
        {
            Name = "我是第一层粘土"
        });

        // 把值些进
        a3[0] = a1;

        var str = a3.ToString();

        return a3;
    }

    public class TestLong
    {
        public long? Property { get; set; } = 10;
    }

    public void 修改Swagger标题描述([FromServices] ISwaggerProvider swaggerProvider)
    {
        // 通过依赖注入 ISwaggerProvider 接口
        var openApiDocument = swaggerProvider.GetSwagger("Default"); // 获取 Default 分组名文档

        // 直接修改即可
        openApiDocument.Info.Title = "我是新标题";
        openApiDocument.Info.Description = "我是新描述";
    }

    public void 测试AES加解密()
    {
        // 测试 AES 加解密
        var key = "7a23b8b759fe43b494c3b456d41383b0"; // 密钥，长度必须为24位或32位
        var xx = "+t282cXHBrhdCUaLo0g0ktR+g9QOfhwuOYH7x6k9ReY=";
        var cd = AESEncryption.Decrypt(xx, key);

        var aesHash = AESEncryption.Encrypt("百小僧", key); // 加密
        var str2 = AESEncryption.Decrypt(aesHash, key); // 解密

        // 加密
        var originBytes = File.ReadAllBytes("image.png"); // 读取源文件内容
        var encryptBytes = AESEncryption.Encrypt(originBytes, "123456");
        encryptBytes.CopyToSave("image.加密.png");

        // 解密
        var encryptBytes2 = File.ReadAllBytes("image.加密.png"); // 读取加密文件内容
        var originBytes2 = AESEncryption.Decrypt(encryptBytes2, "123456");
        originBytes2.CopyToSave("image.真实.png");
    }

    [HttpGet]
    public void 测试CancellationToken参数(CancellationToken cancellationToken)
    {
    }

    public dynamic 测试匿名类嵌套Clay()
    {
        var package = Clay.Object(new
        {
            Name = "我是第一层",
            Age = 20,
            More = new
            {
                Address = "广东省中山市"
            }
        });

        var a3 = Clay.Object(new object[] { });

        a3[0] = new
        {
            Name = "明细1"
        };

        a3[1] = new
        {
            Name = "明细2"
        };

        a3[2] = package;

        a3[3] = new
        {
            package
        };

        var policy = Clay.Object(new
        {
            search = new
            {
                hotel_id = 10,
                check_in_date = DateTime.Now
            },
            package,
            package1 = new
            {
                package,
                a3
            }
        });

        return policy;
    }

    public Dictionary<string, object> 测试粘土对象空值()
    {
        var package = Clay.Parse("""
            {
            	"hotel_id": "usg1",
            	"room_details": {
            		"room_code": "100",
            		"rate_plan_code": "25d967dae0fc",
            		"rate_plan_description": null,
            		"description": "Standard Room",
            		"food": 3,
            		"non_refundable": false,
            		"room_type": "double",
            		"room_view": "",
            		"beds": {
            			"double": 1
            		},
            		"supplier_description": "Standard - 1 Queen Bed",
            		"non_smoking": null,
            		"room_gender": null,
            		"benefits": null,
            		"floor": null,
            		"amenitites": null
            	},
            	"booking_key": "91fefa5f",
            	"room_rate": 1.03,
            	"room_rate_currency": "USD",
            	"client_commission": 0,
            	"client_commission_currency": "USD",
            	"chargeable_rate": 1.03,
            	"chargeable_rate_currency": "USD",
            	"cancellation_policy": {
            		"remarks": "Swimming pool will be closed from June 9 to June 20",
            		"cancellation_policies": [{
            			"penalty_percentage": 0,
            			"date_from": "2024-04-16T00:00:00Z",
            			"date_to": "2024-04-20T00:00:00Z"
            		},
            		{
            			"penalty_percentage": 100,
            			"date_from": "2024-04-20T00:00:00Z",
            			"date_to": "2024-04-21T00:00:00Z"
            		}]
            	},
            	"rate_type": "net",
            	"daily_number_of_units": null,
            	"created_at": "2024-04-16T10:54:35.30403424Z"
            }
            """);

        var obj = new
        {
            search = new
            {
                hotel_id = "usg1",
                check_in_date = "2024-04-20",
                check_out_date = "2024-04-21",
                room_count = 1,
                adult_count = 1
            },
            package = package,
        };

        var clay = Clay.Object(obj);
        var str = clay.ToString();
        var res1 = clay.Solidify<dynamic>();
        Dictionary<string, object> dic = clay.ToDictionary();

        return dic;
    }

    /// <summary>
    /// 测试粘土对象日志监听
    /// </summary>
    /// <returns></returns>
    [LoggingMonitor]
    public dynamic TestClayMonitor()
    {
        var clay = Clay.Parse("""
            {
            	"name": "Furion",
            	"age": 4,
            	"products": [{
            		"name": "Furion",
            		"author": "百小僧"
            	},
            	{
            		"name": "Layx",
            		"author": "百小僧"
            	}],
            }
            """);

        return clay;
    }

    public void 测试忽略路由参数绑定([BindNever] string never, int id)
    {
    }

    public void 测试忽略Body参数绑定([BindNever] BindNeverModel model, string id)
    {

    }

    public async Task<string> 测试粘土对象和模板引擎()
    {
        var sql = @"
@{
    IEnumerable<dynamic> data = Model.AsEnumerable();
    var names = data.Select(u=> u.name);

    foreach(var name in names)
    {
        @:update table set isSync = 1 where name = '@name';
    }
}

@{
    IEnumerable<dynamic> data2 = Model.AsEnumerable();
    var nameStrings = string.Join(""', '"", data2.Select(u=> u.name));

    @:update table set isSync = 1 where name in ('@nameStrings');
}

@foreach(var item in Model)
{
    @:insert into table(member_id, site_id) values(@item.member_id, @item.site_id);

    @foreach(var subItem in item.goods_list)
    {
        @:insert into table(order_id, goods_id) values(@subItem.order_id, @subItem.goods_id);
    }
}";

        dynamic clay = Clay.Parse("""
                    [{
                        "member_id": 69697,
                        "site_id": 1,
                        "remark": "",
                        "order_id": 344,
                        "order_no": "1202405051550696970001",
                        "order_status": 3,
                        "name": "百签科技（广东）有限公司",
                        "mobile": "13800138000",
                        "telephone": "",
                        "address": "广东省中山市",
                        "full_address": "广东省中山市西区",
                        "create_time": 1714895456,
                        "pay_money": "148.20",
                        "buyer_message": "",
                        "drug_code": null,
                        "goods_list": [
                            {
                                "order_id": 344,
                                "goods_id": 816503,
                                "num": 60,
                                "price": "2.60",
                                "real_goods_money": "148.20",
                                "refund_real_money": "0.00",
                                "country_code": "ZHONGSHAN",
                                "goods_code": "YPJN0000776",
                                "third_id": "SPH00008614"
                            }
                        ]
                    },
                    {
                        "member_id": 69698,
                        "site_id": 1,
                        "remark": "",
                        "order_id": 344,
                        "order_no": "1202405051550696970002",
                        "order_status": 3,
                        "name": "百签科技（广东）有限公司",
                        "mobile": "13800138000",
                        "telephone": "",
                        "address": "广东省中山市",
                        "full_address": "广东省中山市西区",
                        "create_time": 1714895456,
                        "pay_money": "148.20",
                        "buyer_message": "",
                        "drug_code": null,
                        "goods_list": [
                            {
                                "order_id": 344,
                                "goods_id": 816503,
                                "num": 60,
                                "price": "2.60",
                                "real_goods_money": "148.20",
                                "refund_real_money": "0.00",
                                "country_code": "ZHONGSHAN",
                                "goods_code": "YPJN0000776",
                                "third_id": "SPH00008614"
                            }
                        ]
                    }]
                    """);

        IEnumerable<dynamic> query = clay.AsEnumerable();

        var order_nos = query.Select(u => u.order_no).ToList();

        //var result = await viewEngine.RunCompileAsync(sql, clay);
        var result = await sql.RunCompileAsync((object)clay);
        return result;
    }

    [SwaggerIgnore]
    public void 测试Swagger忽略()
    {
    }

    public void 测试Swagger忽略2(TestSwaggerIgnore model)
    {
    }

    [HttpGet]
    public List<string> 测试URL数组参数([FromQuery][FlexibleArray<string>] List<string> status)
    {
        return status;
    }

    public bool 测试PBKDF2加密比较()
    {
        // 测试 PBKDF2 加密，比较
        var pbkdf2Hash = PBKDF2Encryption.Encrypt("百小僧");  // 加密
        var isEqual = PBKDF2Encryption.Compare("百小僧", pbkdf2Hash); // 比较
        return isEqual;
    }

    public DateOnly 测试DateOnly格式化()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }

    public TimeOnly 测试TimeOnly格式化()
    {
        return TimeOnly.FromDateTime(DateTime.Now);
    }

    public void 测试DefaultValue(TestDefaultValue value)
    {

    }

    public void 测试AOP([FromServices] ITestService2 service)
    {
        service.SayHello("ddd");
    }

    public DateTime 测试时间本地化1()
    {
        return DateTime.UtcNow;
    }

    public DateTimeOffset 测试时间本地化2()
    {
        return DateTimeOffset.UtcNow;
    }

    public KSortSignature 生成签名数据()
    {
        // 应用标识/密钥
        var appId = "ca36cb2858ce3517df772ec34ce92f21";
        var appKey = "95e4a4f651c2d62679c4c150f2e39f4a";

        // 本次提交命令（标识符）
        var command = "add.user";

        // 序列化需要签名的数据
        var data = JsonSerializer.Serialize(new { id = 1, name = "Furion" });

        return KSortEncryption.Encrypt(appId, appKey, command, data);
    }

    [Consumes("text/plain")]
    public bool 比较签名数据([FromBody] string body)
    {
        var kSortSignature = JsonSerializer.Deserialize<KSortSignature>(body);

        return KSortEncryption.Compare(kSortSignature);
    }

    [Consumes("text/plain")]
    public bool 比较签名数据2([FromBody] string body)
    {
        return KSortEncryption.Compare(body);
    }
}


public interface ITestService2
{
    string SayHello(string word);
}

[Injection(Proxy = typeof(LogDispatchProxy))]
public class TestService2 : ITestService2, ITransient
{
    public string SayHello(string word)
    {
        return $"Hello {word}";
    }
}


public class LogDispatchProxy : AspectDispatchProxy, IDispatchProxy
{
    /// <summary>
    /// 当前服务实例
    /// </summary>
    public object Target { get; set; }

    /// <summary>
    /// 服务提供器，可以用来解析服务，如：Services.GetService()
    /// </summary>
    public IServiceProvider Services { get; set; }

    /// <summary>
    /// 拦截方法
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override object Invoke(MethodInfo method, object[] args)
    {
        Console.WriteLine("SayHello 方法被调用了");

        var result = method.Invoke(Target, args);

        Console.WriteLine("SayHello 方法返回值：" + result);

        return result;
    }

    // 异步无返回值
    public async override Task InvokeAsync(MethodInfo method, object[] args)
    {
        Console.WriteLine("SayHello 方法被调用了");

        var task = method.Invoke(Target, args) as Task;
        await task;

        Console.WriteLine("SayHello 方法调用完成");
    }

    // 异步带返回值
    public async override Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
    {
        Console.WriteLine("SayHello 方法被调用了");

        var taskT = method.Invoke(Target, args) as Task<T>;
        var result = await taskT;

        Console.WriteLine("SayHello 方法返回值：" + result);

        return result;
    }
}


public class BindNeverModel
{
    public int Name { get; set; }
}

public class TestModel
{
    public string Name { get; set; }
    public int[] Items { get; set; }
}

public class TestSwaggerIgnore
{
    public string Name { get; set; }

    [SwaggerIgnore]
    public int[] Items { get; set; }
}

public class TestDefaultValue
{
    [DefaultValue(true)]
    public bool Bool1 { get; set; }

    [DefaultValue(typeof(bool), "true")]
    public bool Bool2 { get; set; }

    ///<example>null</example>
    public bool? Bool3 { get; set; }

    ///<example>"[]"</example>
    public List<string> List1 { get; set; }
}
