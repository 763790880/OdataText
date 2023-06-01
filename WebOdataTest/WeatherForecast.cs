using System.ComponentModel.DataAnnotations;

namespace WebOdataTest
{
    public class WeatherForecast
    {
        
        public DateTime Date { get; set; }
        [Key]
        public int TemperatureC { get; set; }
        public int Week { get; set; }

        public int TemperatureF { get; set; };

        public string? Summary { get; set; }
    }
}