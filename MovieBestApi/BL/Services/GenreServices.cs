using Microsoft.EntityFrameworkCore;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;

namespace MovieBestAPI.Services
{
    public class GenreServices : IGenreService
    {
        private readonly AppDbContext context;

        public GenreServices(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await context.Genres.OrderBy(g=>g.Name).ToListAsync();
            
        }
        public async Task<Genre> Add(Genre genre)
        {
            await context.Genres.AddAsync(genre);
            context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
             context.Remove(genre);
            context.SaveChanges();
            return genre;

        }

  

        public  Genre Update(Genre genre)
        {
            context.Genres.Update(genre);
            context.SaveChanges();
            return genre;

        }

        public async Task<Genre> GetById(int id)
        {
          return  await context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsValidGenre(int id)
        {
            return context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
