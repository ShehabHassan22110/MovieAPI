using Microsoft.EntityFrameworkCore;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;

namespace MovieBestAPI.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly AppDbContext context;

        public PeopleService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<People>> GetAll()
        {
            return await context.People.ToListAsync();
        }

        public async Task<People> GetById(int id)
        {
            return await context.People.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<People> Create(People people)
        {
            context.Entry(people).State = EntityState.Added;
            context.SaveChanges();
            return await context.People.OrderBy(a => a.Id).LastOrDefaultAsync();
        }

        public People Update(People people)
        {
            context.Entry(people).State = EntityState.Modified;
            context.SaveChanges();

            return people;
        }
        public People Delete(People people)
        {
            context.Remove(people);
            context.SaveChanges();
            return people;
        }

     

      
    }
}
