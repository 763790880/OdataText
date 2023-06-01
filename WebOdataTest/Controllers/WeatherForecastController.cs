using System.Linq.Dynamic.Core;

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
        /// <returns> </returns>
        [HttpGet]
        //[EnableQuery(EnsureStableOrdering = true)]
        [a(EnsureStableOrdering = true)]
        public IActionResult Get()
        {
            IEnumerable<WeatherForecast> values = new List<WeatherForecast>()
            {
                new WeatherForecast() { Summary="阴天",TemperatureC=21,Week=3} ,
                new WeatherForecast() { Summary="雨天",TemperatureC=23,Week=2} ,
                new WeatherForecast() { Summary="晴天",TemperatureC=22,Week=1} ,
                new WeatherForecast() { Summary="晴天1",TemperatureC=25,Week=1} ,
            };
            //first 内存linq排序正常
            values = values.OrderBy(x => x.Week)
                .AsQueryable()
                .ToList();
            //seound Odata协议进行拦截响应
            //解决方式：
            /*
             * 请求参数带上查询条件http://127.0.0.1:5168/odata/WeatherForecast?$top=20&$count=true&$orderby=Week
             * 重写特性拦截器中强制week排序，弊端：客户端无法灵活排序。  不建议
             */
            return Ok(values);
        }
    }

    public class aAttribute : EnableQueryAttribute
    {
        public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
        {
            // 强制按 Week 属性排序
            queryable = queryable.OrderBy("Week");
            return queryable;
        }
    }
}
