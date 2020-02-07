using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecastApp
{
    /// <summary>
    /// Forecast result from Open Weather App.
    /// </summary>
    public class ForecastResult
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public Forecast[] list { get; set; }
    }
  
    public class Forecast
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public Weather[] weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Snow snow { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public float temp_kf { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public float ThreeHours { get; set; }      
    }

    public class Snow
    {
        [JsonProperty("3h")]
        public float ThreeHours { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
   

}
