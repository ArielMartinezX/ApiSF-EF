namespace Application.Interfaces
{
    public interface IDeleteFighterCommand : IAsyncCommand
    {
        IAsyncCommand Configure(string id);
    }
}
