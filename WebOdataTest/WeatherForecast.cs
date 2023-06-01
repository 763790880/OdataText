using System.ComponentModel.DataAnnotations;

namespace WebOdataTest
{
    public class WeatherForecast
    {
        
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        [Key]
        public int Week { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}