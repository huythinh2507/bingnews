using System;

namespace Bing_News
{
    public class Sports
    {
        public Sports()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}