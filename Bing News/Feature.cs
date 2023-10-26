namespace Bing_News
{
    internal class Feature
    {
        public Feature()
        {
        }

        public Categories Categories { get; set; }
        public Topics Topics { get; set; }
        public News_Aggregation News_aggregation { get; set; }
        public Trending_Stories Trending_stories { get; internal set; }
        public Localized_news Localized_news { get; internal set; }
        public Search_Intergration Search_integration { get; internal set; }
        public Multimediacontent Multimediacontent { get; internal set; }
        public Personalization Personalization { get; internal set; }
    }
}