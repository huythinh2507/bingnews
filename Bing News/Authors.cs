using System.Collections.Generic;

namespace Bing_News
{
    public class Authors
    {
        public Authors()
        {
        }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<Articles> Articles { get; set; }
    }
}
