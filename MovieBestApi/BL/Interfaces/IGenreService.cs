using MovieBestAPI.Models;

namespace MovieBestAPI.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsValidGenre(int id);
    }
}
