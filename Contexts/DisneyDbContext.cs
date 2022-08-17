using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Contexts
{
    public class DisneyDbContext : DbContext
    {
        public DbSet<Character>? Characters { set; get; }
        public DbSet<MovieSerie>? Media { set; get; }
        public DbSet<Genre>? Genres { set; get; }

        public DisneyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var disneyDb_ConnString = configuration.GetConnectionString("DisneyDbConnection");
            optionsBuilder.UseSqlServer(disneyDb_ConnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasMany(x => x.MoviesAndSeries)
                .WithMany(x => x.Characters)
                .UsingEntity(j => j.ToTable("CharacterMoviesAndSeries"));

            modelBuilder.Entity<MovieSerie>()
                .HasMany(x => x.Characters)
                .WithMany(x => x.MoviesAndSeries)
                .UsingEntity(j => j.ToTable("MovieSerieCharacters"));
        }
    }
}
