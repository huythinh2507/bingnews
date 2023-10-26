using System;

namespace Bing_News
{
    public class Sci_tech
    {
        public Sci_tech()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}