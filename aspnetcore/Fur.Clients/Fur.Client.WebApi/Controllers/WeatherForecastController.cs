using Fur.Versatile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fur.Client.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestService _testService;
        private readonly ITestServiceOfT<Test> _testServiceOfT;
        private readonly ITestServiceOfT<Test2> _testServiceOfT2;
        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , ITestService testService
            , ITestServiceOfT<Test> testServiceOfT
             , ITestServiceOfT<Test2> testServiceOfT2)
        {
            _logger = logger;
            _testService = testService;
            _testServiceOfT = testServiceOfT;
            _testServiceOfT2 = testServiceOfT2;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route(nameof(GetName))]
        public string GetName()
        {
            return _testService.GetName();
        }

        [HttpGet]
        [Route(nameof(GetNameOfT))]
        public string GetNameOfT()
        {
            return _testServiceOfT.GetName() + _testServiceOfT2.GetName();
        }
    }
}
