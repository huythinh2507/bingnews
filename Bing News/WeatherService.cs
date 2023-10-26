using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace Bing_News
{
    public class WeatherService
    {

        public WeatherService(WeatherDbContext context)
        {
        }
        public async Task SaveWeatherToDatabaseORM(int noofdays, string location)
        {
            // Get the weather data
            List<Weather> weatherList = await GetWeatherAsync(noofdays, location);
            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");

            // Save the weather data to the database
            using (var context = new WeatherDbContext(optionsBuilder.Options))
            {
                foreach (var weather in weatherList)
                {
                    // Check if the weather data already exists in the database
                    var existingWeather = await context.Weather
                        .Where(w => w.Location == weather.Location && w.Date == weather.Date)
                        .FirstOrDefaultAsync();

                    // If the weather data doesn't exist, add it to the database
                    if (existingWeather == null)
                    {
                        context.Weather.Add(weather);
                    }
                }
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<Weather>> GetSavedWeatherData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");

            using (var context = new WeatherDbContext(optionsBuilder.Options))
            {
                return await context.Weather.ToListAsync();
            }
        }

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
        public List<Weather> GetWeatherData(int noofdays, string location)
        {
            SaveWeatherToDatabaseORM(noofdays, location).Wait();
            List<Weather> savedData = GetSavedWeatherData().Result;
            return savedData;
        }
        public async Task DeleteOldWeatherData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");
            using (var context = new WeatherDbContext(optionsBuilder.Options))
            {
                    // Get all the weather data
                    var allWeatherData = context.Weather;

                    // Remove all the weather data from the database
                    context.Weather.RemoveRange(allWeatherData);

                    // Save the changes to the database
                    await context.SaveChangesAsync();
                }
            }
        }
}
