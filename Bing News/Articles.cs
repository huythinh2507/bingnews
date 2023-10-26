using System;

namespace Bing_News
{
    public class Articles
    {
        public Articles()
        {
        }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Authors Author { get; set; }
    }
}