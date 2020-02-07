using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WeatherForecastApp
{
    public class OpenWeatherMapClient
    {
        HttpClient client;
        private string apiKey;
        public OpenWeatherMapClient(string apiKey)
        {
            // Init client and set our API client
            client = new HttpClient();
            this.apiKey = apiKey;
            client.BaseAddress = new Uri("http://api.openweathermap.org");           
        }

        public async Task<ForecastResult> GetForecast(double latitude, double longitude)
        {
            var uri=new UriBuilder($"{client.BaseAddress.ToString()}/data/2.5/forecast");
            uri.Query = $"APPID={this.apiKey}&lat={latitude}&lon={longitude}&units=metric";
            var response = await client.GetAsync(uri.ToString());
            var stringResult = await response.Content.ReadAsStringAsync();
            var forecastResult = JsonConvert.DeserializeObject<ForecastResult>(stringResult);
            return forecastResult;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
