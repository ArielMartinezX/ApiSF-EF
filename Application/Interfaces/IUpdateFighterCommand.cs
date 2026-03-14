using Models;

namespace Application.Interfaces
{
    public interface IUpdateFighterCommand : IAsyncCommand
    {
        IAsyncCommand Configure(string id, Fighter fighterToUpdate);
    }
}
