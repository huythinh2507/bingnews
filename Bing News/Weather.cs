namespace Bing_News
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using static System.Net.WebRequestMethods;

    public class Weather
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        public string Location { get; set; }
        public string CurrentTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }

        public string WeatherCondition { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Humidity { get; set; }
        public string Date { get; set; }
     
        public static async Task<List<Weather>> GetWeatherAsync(int noofdays, string location)
        {
            string ApiKey = "SEQ274PQMK9WSX6EAXC76CRBK";
            string BaseUrl = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
            string json = System.IO.File.ReadAllText("C:\\Users\\thinh\\source\\repos\\Bing News\\Bing News\\api_mapping.json");
            JObject apiMapping = JObject.Parse(json);
            string url = $"{BaseUrl}{location}?unitGroup=uk&key={ApiKey}";
            HttpClient _httpClient = new HttpClient();
            string data = await _httpClient.GetStringAsync(url);
            JObject weatherData = JObject.Parse(data);
            JArray days = (JArray)weatherData["days"];
            
            List<Weather> WeatherList = new List<Weather>();

            for (int i = 0; i < noofdays; i++)
            {
                Weather weatherInfo = new Weather
                {
                    Location = location,
                    CurrentTemperature = $"{days[i].SelectToken(apiMapping["API1"]["Temperature"].ToString())}°C",
                    MaxTemperature = $"{days[i].SelectToken(apiMapping["API1"]["MaxTemperature"].ToString())}°C",
                    MinTemperature = $"{days[i].SelectToken(apiMapping["API1"]["MinTemperature"].ToString())}°C",
                    WeatherCondition = $"{days[i].SelectToken(apiMapping["API1"]["WeatherCondition"].ToString())}",
                    Description = $"{days[i].SelectToken(apiMapping["API1"]["Description"].ToString())}",
                    Icon = $"{days[i].SelectToken(apiMapping["API1"]["Icon"].ToString())}",
                    Humidity = $"{days[i].SelectToken(apiMapping["API1"]["Humidity"].ToString())}",
                    Date = $"{days[i].SelectToken(apiMapping["API1"]["Date"].ToString())}"
                };
                    WeatherList.Add(weatherInfo);
            }
            return WeatherList;
        }
        public static async Task<List<Weather>> GetTaskAsyncFromOpenWeather(int noofdays, string location)
        {
            HttpClient client = new HttpClient();
            string apiKey = "f29392bf4044b4ef3b7ae570aad4dba3";
            string requestUri = $"https://api.openweathermap.org/data/2.5/forecast?q{location}&appid={apiKey}&cnt={noofdays}&units=metric";
            string data = await client.GetStringAsync(requestUri);
            JObject weatherData = JObject.Parse(data);
            List<Weather> WeatherList = new List<Weather>();
            string json = System.IO.File.ReadAllText("C:\\Users\\thinh\\source\\repos\\Bing News\\Bing News\\api_mapping.json");
            JObject apiMapping = JObject.Parse(json);
            Weather weatherInfo = new Weather()
            {
                Location = location,
                CurrentTemperature = $"{weatherData.SelectToken(apiMapping["API2"]["Temperature"].ToString())}°C",
                MaxTemperature = $"{weatherData.SelectToken(apiMapping["API2"]["MaxTemperature"].ToString())}°C",
                MinTemperature = $"{weatherData.SelectToken(apiMapping["API2"]["MinTemperature"].ToString())}°C",
                WeatherCondition = null,
                Description = $"{weatherData["weather"][0]["description"]}",
                Icon = $"{weatherData["weather"][0]["icon"]}",
                Humidity = $"{weatherData.SelectToken(apiMapping["API2"]["Humidity"].ToString())}",
            };
            WeatherList.Add(weatherInfo);
            return WeatherList;
        }
        public List<Weather> GetWeatherInfoFromOpenWeather()
        {
            return GetTaskAsyncFromOpenWeather(7, "Ho Chi Minh").Result;
        }
        public static async Task<List<Weather>> GetMergedWeatherAsync(int noofdays, string location)
        {
            // Get weather data from the first API
            List<Weather> weatherList1 = await GetWeatherAsync(noofdays, location);

            // Get weather data from OpenWeatherMap API
            List<Weather> weatherList2 = await GetTaskAsyncFromOpenWeather(7, "Ho Chi Minh");

            // Merge the two lists
            List<Weather> mergedList = new List<Weather>();
            mergedList.AddRange(weatherList1);
            mergedList.AddRange(weatherList2);

            return mergedList;
        }

        public List<Weather> GetFinalWeatherInfo()
        {
            return GetMergedWeatherAsync(5, "Dalat").Result;
        }
        public List<Weather> GetWeatherInfo(int noOfDays, string location)
        {
            return GetWeatherAsync(noOfDays, location).GetAwaiter().GetResult();
        }

    }
}
        
