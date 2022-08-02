namespace Furion.Application;

public class TestNotFound : IDynamicApiController
{
    [HttpGet]
    public IActionResult 测试404()
    {
        return new NotFoundResult();
    }

    [HttpGet]
    public IActionResult 测试404_2()
    {
        return new NotFoundObjectResult(new
        {
            Name = "Not Found"
        });
    }
}