
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBestAPI.Models;

namespace MovieBestAPI.Models
{
    public class AppDbContext: DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<TvShow> TvShow { get; set; }
    }
}
