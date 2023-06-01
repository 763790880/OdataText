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
        /// ������Keyʱ������������Ч
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        //[EnableQuery(EnsureStableOrdering = true)]
        [a(EnsureStableOrdering = true)]
        public IActionResult Get()
        {
            IEnumerable<WeatherForecast> values = new List<WeatherForecast>()
            {
                new WeatherForecast() { Summary="����",TemperatureC=21,Week=3} ,
                new WeatherForecast() { Summary="����",TemperatureC=23,Week=2} ,
                new WeatherForecast() { Summary="����",TemperatureC=22,Week=1} ,
                new WeatherForecast() { Summary="����1",TemperatureC=25,Week=1} ,
            };
            //first �ڴ�linq��������
            values = values.OrderBy(x => x.Week)
                .AsQueryable()
                .ToList();
            //seound OdataЭ�����������Ӧ
            //�����ʽ��
            /*
             * ����������ϲ�ѯ����http://127.0.0.1:5168/odata/WeatherForecast?$top=20&$count=true&$orderby=Week
             * ��д������������ǿ��week���򣬱׶ˣ��ͻ����޷��������  ������
             */
            return Ok(values);
        }
    }

    public class aAttribute : EnableQueryAttribute
    {
        public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
        {
            // ǿ�ư� Week ��������
            queryable = queryable.OrderBy("Week");
            return queryable;
        }
    }
}
