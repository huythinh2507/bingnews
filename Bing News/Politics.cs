using System;

namespace Bing_News
{
    public class Politics
    {
        public Politics()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}