using Models;

namespace Application.Interfaces
{
    public interface IGetFightersCommand : IAsyncCommand
    {
        public ICollection<Fighter> Fighters { get; set; }

    }
}
