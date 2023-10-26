namespace Bing_News
{
    public class Categories
    {
        public Categories()
        {
        }
        public TopStory Topstory { get; internal set; }
        public World World { get; internal set; }
        public Sci_tech Sci_tech { get; set; }
        public Sports Sports { get; set; }
        public Entertainment Entertainment { get; set; }
        public Politics Politics { get; set; }
        public Business Business { get; set; }
    }
}