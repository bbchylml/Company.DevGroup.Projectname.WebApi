using System;

namespace Company.DevGroup.Projectname.Models
{
    /// <summary>
    /// WeatherForecast
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
