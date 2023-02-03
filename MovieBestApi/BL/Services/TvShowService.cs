using Microsoft.EntityFrameworkCore;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;

namespace MovieBestAPI.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly AppDbContext context;

        public TvShowService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TvShow>> GetAll()
        {
            return await context.TvShow.ToListAsync();
        }

        public async Task<TvShow> GetById(int id)
        {
            return await context.TvShow.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<TvShow> Create(TvShow tvShow)
        {
            context.Entry(tvShow).State = EntityState.Added;
            context.SaveChanges();
            return tvShow;
        }
        public TvShow Update(TvShow tvShow)
        {
            context.Entry(tvShow).State = EntityState.Modified;
            context.SaveChanges();
            return tvShow;
        }

        public TvShow Delete(TvShow tvShow)
        {
            context.TvShow.Remove(tvShow);
            context.SaveChanges();
            return tvShow;
        }

       

       
    }
}
