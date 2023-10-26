using System;

namespace Bing_News
{
    public class World
    {
        public World()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}