using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;

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
        [EnableQuery(EnsureStableOrdering = false)]
        public IActionResult Get(ODataQueryOptions<WeatherForecast> options)
        {
            ///odata/WeatherForecast?$top=20&$skip=3&$count=true
            IEnumerable<WeatherForecast> values = new List<WeatherForecast>()
            {
                new WeatherForecast() { Summary="阴天",TemperatureC=21,Week=3} ,
                new WeatherForecast() { Summary="雨天",TemperatureC=23,Week=2} ,
                new WeatherForecast() { Summary="晴天",TemperatureC=22,Week=1} ,
                new WeatherForecast() { Summary="晴天1",TemperatureC=25,Week=1} ,
            };
            values = values.OrderBy(x => x.Summary.Contains("晴")).AsQueryable();
            //最终问题
            values = options.ApplyTo(values.AsQueryable()) as IEnumerable<WeatherForecast>;
            var newValues= values.ToList();
            //$top=20
            //想对查询出来得20条数据中得某一属性循环赋值
            foreach (var value in newValues)
            {
                value.TemperatureF = 5;
            }
            //问题是最终结果会被再次执行skip
            return Ok(newValues);
        }
    }
}