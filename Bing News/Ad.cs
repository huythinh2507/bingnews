using System;
using System.Collections.Generic;
namespace Bing_News
{
    public class Ad
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }   
        public string AdSource { get; set; }
        public List<Ad> Advertisement()
        {
            List<Ad> adlist = new List<Ad>();
            Ad adinfo = new Ad()
            {
                Title = "Your Ad Title",
                Content = "Your Ad Content",
                URL = "https://www.yourwebsite.com",
                ImagePath = "C:\\path\\to\\your\\image.jpg",
                VideoPath = "C:\\path\\to\\your\\video.mp4",
                AdSource = "Your Ad Source"
            };
            adlist.Add(adinfo);
            return adlist;
        }

        public List<Ad> DisplayAd()
        {
            return Advertisement();
        }

    }
}