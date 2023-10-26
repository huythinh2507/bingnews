using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bing_News
{

    public class News_Aggregation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SourceNode { get; set; }
        public string Headline { get; set; }
        public string PostingTime { get; set; }
        public string ImageUrl { get; set; }
        public string Sourcesimg { get; set; }
        public int Likes { get; private set; } = 0;
        public int Dislikes { get; private set; } = 0;


        public List<News_Aggregation> GetNewsSource()
        {           
            var PostSource = new List<News_Aggregation>();
            string[] urls = { "https://news.google.com"};
            foreach (string url in urls)
            {
                // Send a GET request
                var web = new HtmlWeb();
                var doc = web.Load(url);              
                var articles = doc.DocumentNode.Descendants("article");
                foreach (var article in articles)
                {
                    var sourceNode = article.Descendants("div").FirstOrDefault(d => d.GetAttributeValue("class", "").Contains("vr1PYe"))?.InnerText ?? System.Net.WebUtility.HtmlDecode(article.Descendants("span").FirstOrDefault()?.InnerText);
                    var headline = System.Net.WebUtility.HtmlDecode(article.Descendants("h4").FirstOrDefault()?.InnerText);
                    if (headline == null)
                    {
                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(article.OuterHtml);
                        var headlineNode = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='tease-card__headline']");
                    
                        if (headlineNode != null)
                        {
                            headline = System.Net.WebUtility.HtmlDecode(headlineNode.InnerText);
                        }
                    }

                    var postingTime = article.Descendants("time").FirstOrDefault()?.InnerText;
                    var imageUrl = article.Descendants("img").FirstOrDefault()?.GetAttributeValue("src", "");
                    var sourcesimage = article.Descendants("img").FirstOrDefault(o => o.GetAttributeValue("class", "").Contains("qEdqNd"))?.GetAttributeValue("src", "");

                    PostSource.Add(new News_Aggregation
                    {
                        SourceNode = sourceNode,
                        Headline = headline,
                        PostingTime = postingTime,
                        ImageUrl = imageUrl,
                        Sourcesimg = sourcesimage
                    });
                        }
            }
            return PostSource;
        }
        public async Task SaveNewsToDatabase()
        {
            // Get the data
            List<News_Aggregation> newsList = GetNewsSource();
            var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");

            // Save the weather data to the database
            using (var context = new NewsDbContext(optionsBuilder.Options))
            {
                foreach (var PostSource in newsList)
                {
                    // Check if the data already exists in the database
                    var existingNews = await context.News
                        .Where(w => w.Headline == PostSource.Headline)
                        .FirstOrDefaultAsync();

                    // If the data doesn't exist, add it to the database
                    if (existingNews == null)
                    {
                        context.News.Add(PostSource);
                    }
                }
                await context.SaveChangesAsync();
            }

        }
        public async Task<List<News_Aggregation>> GetSavedNewsData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");

            using (var context = new NewsDbContext(optionsBuilder.Options))
            {
                return await context.News.ToListAsync();
            }
        }
        public List<News_Aggregation> GetNewsData()
        {
            SaveNewsToDatabase().Wait();
            List<News_Aggregation> savedData = GetSavedNewsData().Result;
            return savedData;
        }
    }
}

