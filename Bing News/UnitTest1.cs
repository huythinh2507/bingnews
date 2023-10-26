using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using Microsoft.Identity.Client;

namespace Bing_News
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBingNewsIni()
        {
            var bingnewsService = new BingnewsService();
            var _root = bingnewsService.CreateDefaultBingNews();
            Assert.IsNotNull(_root);
        }
        [TestMethod]
        public void TestNewsSource()
        {
            var _root = new BingnewsService().CreateDefaultBingNews().Feature.News_aggregation.GetNewsSource();
            Assert.IsTrue(_root.TrueForAll(a => a.Headline != null));


        }

        [TestMethod]
        public void CreateNewsPost()
        {
            int no_post_to_be_created = 2;
            var _root = new BingnewsService().CreateBingNewsPost(new News_Aggregation(), no_post_to_be_created);
            Assert.AreEqual(2, _root.Count);
        }
        [TestMethod]
        public void GetWeatherInfoFromVisualCrossing()
        {
            int noOfDays = 7;
            string location = "Paris";
            var weather = new BingnewsService()
            .CreateDefaultBingNews()
            .Weather.GetWeatherInfo(noOfDays, location);
            Assert.IsTrue(weather.TrueForAll(
                a => a.CurrentTemperature != null
                && a.MaxTemperature != null
                && a.MinTemperature != null));

            Assert.AreEqual(noOfDays, weather.Count);
            Assert.IsTrue(weather.TrueForAll(a => a.Location == location));
        }
        [TestMethod]
        public void GetWeatherInfoFromOpenWeather()
        {
            var weather = new BingnewsService().CreateDefaultBingNews().Weather.GetWeatherInfo(1, "Ho Chi Minh");
            Assert.IsNotNull(weather);
        }
        [TestMethod]
        public void GetAd()
        {
            var ad = new BingnewsService().CreateDefaultBingNews().Ad.Advertisement();
            Assert.IsNotNull(ad);
        }
        [TestMethod]
        public void GetFinance()
        {
            var finance = new BingnewsService().CreateDefaultBingNews().Finance.GetFinanceInfo();
            Assert.IsNotNull(finance);
        }
        [TestMethod]
        public async Task GetFinance_ReturnsCorrectData()
        {
            // Act

            string url = $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/2023-01-09?adjusted=true&apiKey=Ifj4VX4KLycT96xm_YKpVa5RW_BY7mRo";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var financeData = JObject.Parse(responseString);

            // Assert
            Assert.IsNotNull(financeData);
        }

        [TestMethod]
        public async Task GetWeatherInfoFromDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            var dbContext = new WeatherDbContext(optionsBuilder.Options);

            var weatherService = new WeatherService(dbContext);
            int number_of_days = 7;
            string location = "Paris";

            await weatherService.DeleteOldWeatherData();

            var result = weatherService.GetWeatherData(number_of_days, location);

            Assert.AreEqual(number_of_days, result.Count);
        }
        [TestMethod]
        public async Task GetNewsInfoFromDB()
        {
            var bingnewsService = new BingnewsService();
            var result = bingnewsService.CreateDefaultBingNews()
                .Feature.News_aggregation.GetNewsData();
            Assert.IsNotNull(result);
        }

    }
  
        
}


