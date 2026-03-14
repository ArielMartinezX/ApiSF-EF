using Models;

namespace Application.Interfaces
{
    public interface IFightersRepository
    {
        Task<List<Fighter>> GetFighters();
        Task<Fighter> GetFighterById(Guid id);

        Task<bool> DeleteFighter(Guid id);

        Task<bool> UpdateFighter(Guid id, Fighter fighter);

        Task<Guid> InsertFighter(Fighter fighter);
    }
}
