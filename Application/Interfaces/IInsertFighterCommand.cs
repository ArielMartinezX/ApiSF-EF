using Models;

namespace Application.Interfaces
{
    public interface IInsertFighterCommand : IAsyncCommand
    {
        public Guid NewFighterId { get; }
        IAsyncCommand Configure(Fighter newFighter);
    }
}
