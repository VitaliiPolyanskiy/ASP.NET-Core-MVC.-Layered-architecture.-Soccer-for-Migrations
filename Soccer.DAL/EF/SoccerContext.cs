using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Soccer.DAL.Entities;

namespace Soccer.DAL.EF
{   
    public class SoccerContext : DbContext
    { 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public SoccerContext(DbContextOptions<SoccerContext> options)
                   : base(options)
        {
            //Database.EnsureCreated();
        }
    }
    // Класс необходим исключительно для миграций
    public class SampleContextFactory : IDesignTimeDbContextFactory<SoccerContext>
    {
        public SoccerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SoccerContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new SoccerContext(optionsBuilder.Options);
        }
    }
}
