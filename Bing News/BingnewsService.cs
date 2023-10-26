using System;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;

using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using HtmlAgilityPack;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bing_News
{
    public class BingnewsService
    {
        public BingnewsService()
        {
        }

        public NewsContext CreateDefaultBingNews()
        {
            return new NewsContext
            {
                Feature = new Feature
                {
                    Categories = new Categories
                    {
                        Topstory = new TopStory(),
                        World = new World(),
                        Business = new Business(),
                        Politics = new Politics(),
                        Entertainment = new Entertainment(),
                        Sports = new Sports(),
                        Sci_tech = new Sci_tech()
                    },
                    Topics = new Topics(),
                    News_aggregation = new News_Aggregation(),
                    Trending_stories = new Trending_Stories(),
                    Personalization = new Personalization(),
                    Multimediacontent = new Multimediacontent(),
                    Search_integration = new Search_Intergration(),
                    Localized_news = new Localized_news()
                },
                Article = new Articles(),
                Author = new Authors(),
               
                Weather = new Weather(),
                Ad = new Ad(),
                Finance = new Finance()
            };
        }

        public List<Post> CreateBingNewsPost(News_Aggregation news_Aggregation, int v)
        {
            Post bingnews = new Post();
            var posts = bingnews.CreateBingNewPost(news_Aggregation,v);
            return posts;
        }
        

    }
   
}