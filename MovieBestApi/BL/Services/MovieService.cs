using Microsoft.EntityFrameworkCore;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;

namespace MovieBestAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext context;

        public MovieService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll(int genreId =0)
        {
            return await context.Movies.Include(m=>m.Genre).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await context.Movies.FirstOrDefaultAsync(g => g.Id == id);

        }
        public async Task<Movie> Add(Movie movie)
        {
            await context.AddAsync(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie Update(Movie movie)
        {
            context.Update(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            return movie;
        }

  
    }
}
