using MovieBestAPI.Models;

namespace MovieBestAPI.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(int genreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
