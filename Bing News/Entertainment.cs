using System;

namespace Bing_News
{
    public class Entertainment
    {
        public Entertainment()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}