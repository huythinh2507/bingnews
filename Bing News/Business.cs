using System;

namespace Bing_News
{
    public class Business
    {
        public Business()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}