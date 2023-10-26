using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bing_News
{
    public class Post
    {
        public Guid Id {  get; set; }
        public Post()
        {
            Id = Guid.NewGuid();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public object Likes { get; set; }
        public object Dislikes { get; set; }

     

      
        public List<Post> CreateBingNewPost(News_Aggregation news_Aggregation, int v)
        {
            var posts = new List<Post>();
            var newsSource = news_Aggregation.GetNewsSource();

            if (newsSource != null && newsSource.Count >= v)
            {
                for (int i = 0; i < v; i++)
                {
                    var newsArticle = newsSource[i];
                    var post = new Post
                    {
                        Title = newsArticle.Headline,
                        Content = 
                        $"Source: {newsArticle.SourceNode}" +
                        $"Time: {newsArticle.PostingTime}" +
                        $"Image: {newsArticle.ImageUrl}",
                        Likes = newsArticle.Likes,
                        Dislikes = newsArticle.Dislikes
                    };
                    posts.Add(post);
                }
            }

            return posts;
        }
    }
    
    public class MyDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }

}

  