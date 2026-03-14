using Models;

namespace Application.Interfaces
{
    public interface IHttpSWProxy
    {
        Task<List<SWPeople>> GetAllAsync();
        Task<SWPeople> GetByIdAsync(int id);
    }
}
