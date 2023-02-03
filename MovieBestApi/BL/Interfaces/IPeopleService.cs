using MovieBestAPI.Models;

namespace MovieBestAPI.Interfaces
{
    public interface IPeopleService
    {
        Task<IEnumerable<People>> GetAll();
        Task<People> GetById(int id);
        Task<People> Create(People people);
        People Update(People people);
        People Delete(People people);
    }
}
