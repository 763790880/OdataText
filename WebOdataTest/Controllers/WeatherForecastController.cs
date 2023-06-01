using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebOdataTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 当存在Key时程序内排序无效
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableQuery(EnsureStableOrdering = true)]
        public IActionResult Get()
        {
            IEnumerable<WeatherForecast> values = new List<WeatherForecast>()
            {
                new WeatherForecast() { Summary="阴天",TemperatureC=21,Week=3} ,
                new WeatherForecast() { Summary="雨天",TemperatureC=23,Week=2} ,
                new WeatherForecast() { Summary="晴天",TemperatureC=22,Week=1} ,
                new WeatherForecast() { Summary="晴天1",TemperatureC=25,Week=1} ,
            };
            values = values.OrderBy(x => x.Week).AsQueryable();
            return Ok(values);
        }
    }
}