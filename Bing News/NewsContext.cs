using System.Data.Entity;

namespace Bing_News
{
    public class NewsContext
    {
        public NewsContext()
        {
        }
        internal Feature Feature { get; set; }
        internal Articles Article { get; set; }
        internal Authors Author { get; set; }
        public DbContext Dbcontext { get; set; }
        internal Weather Weather { get; set; }
        internal Ad Ad { get; set; }
        internal Finance Finance { get; set; }
    }
}