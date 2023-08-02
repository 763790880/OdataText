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
        /// ������Keyʱ������������Ч
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[NewEnableQuery(EnsureStableOrdering = false)]
        public IActionResult Get(ODataQueryOptions<WeatherForecast> options)
        {
            ///odata/WeatherForecast?$top=20&$skip=3&$count=true
            IEnumerable<WeatherForecast> values = new List<WeatherForecast>()
            {
                new WeatherForecast() { Summary="����",TemperatureC=21,Week=3} ,
                new WeatherForecast() { Summary="����",TemperatureC=23,Week=2} ,
                new WeatherForecast() { Summary="����",TemperatureC=22,Week=1} ,
                new WeatherForecast() { Summary="����1",TemperatureC=25,Week=1} ,
            };
            values = values.OrderByDescending(x => x.Summary.Contains("��")).AsQueryable();
            //��������
            var orderby = options.OrderBy.RawValue;
            options.SetBaseTypePropertyValue("OrderBy", null);
            values = options.ApplyTo(values.AsQueryable(), new ODataQuerySettings { EnsureStableOrdering =false}) as IEnumerable<WeatherForecast>;
            var newValues = values.ToList();
            //$top=20
            //��Բ�ѯ������20�������е�ĳһ����ѭ����ֵ
            foreach (var value in newValues)
            {
                value.TemperatureF = 5;
            }
            //���������ս���ᱻ�ٴ�ִ��skip
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