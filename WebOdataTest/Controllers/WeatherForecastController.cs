using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using System.Reflection;

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
        //[NewEnableQuery(EnsureStableOrdering = false)]
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
            values = values.OrderByDescending(x => x.Summary.Contains("晴")).AsQueryable();
            //最终问题
            var orderby = options.OrderBy.RawValue;
            options.SetBaseTypePropertyValue("OrderBy", null);
            values = options.ApplyTo(values.AsQueryable(), new ODataQuerySettings { EnsureStableOrdering =false}) as IEnumerable<WeatherForecast>;
            var newValues = values.ToList();
            //$top=20
            //想对查询出来得20条数据中得某一属性循环赋值
            foreach (var value in newValues)
            {
                value.TemperatureF = 5;
            }
            //问题是最终结果会被再次执行skip
            return Ok(newValues);
        }
        public class NewEnableQueryAttribute : EnableQueryAttribute
        {
            public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
            {
                queryable=base.ApplyQuery(queryable, queryOptions);
                foreach (var value in queryable as IQueryable<WeatherForecast>) 
                {
                    value.TemperatureF = 5;
                }
                return queryable;
            }
        }
    }
    public static class exted
    {
        private static BindingFlags _bindingFlags { get; }
            = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;
        public static void SetBaseTypePropertyValue(this object obj, string propertyName, object value)
        {
            obj.GetType().BaseType.GetProperty(propertyName, _bindingFlags)?.SetValue(obj, value);
        }
    }
}