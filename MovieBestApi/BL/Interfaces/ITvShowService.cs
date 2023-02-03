using MovieBestAPI.Models;

namespace MovieBestAPI.Interfaces
{
    public interface ITvShowService
    {
        Task<IEnumerable<TvShow>> GetAll();
        Task<TvShow> GetById(int id);
        Task<TvShow> Create(TvShow tvShow);
        TvShow Update(TvShow tvShow);
        TvShow Delete(TvShow tvShow);
    }
}
