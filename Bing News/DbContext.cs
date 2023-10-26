using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Bing_News
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }

        public DbSet<News_Aggregation> News { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }

    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\thinh\\OneDrive\\Documents\\SQL2022.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}