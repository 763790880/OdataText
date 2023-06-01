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
        /// ������Keyʱ������������Ч
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableQuery(EnsureStableOrdering = false)]
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
            values = values.OrderBy(x => x.Summary.Contains("��")).AsQueryable();
            //��������
            values = options.ApplyTo(values.AsQueryable()) as IEnumerable<WeatherForecast>;
            var newValues= values.ToList();
            //$top=20
            //��Բ�ѯ������20�������е�ĳһ����ѭ����ֵ
            foreach (var value in newValues)
            {
                value.TemperatureF = 5;
            }
            //���������ս���ᱻ�ٴ�ִ��skip
            return Ok(newValues);
        }
    }
}