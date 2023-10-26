using System;
using System.ComponentModel;

namespace Bing_News
{
    public class TopStory
    {
        public TopStory()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}