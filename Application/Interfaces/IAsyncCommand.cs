namespace Application.Interfaces
{
    public interface IAsyncCommand
    {
        string Message { get; set; }
        bool Success { get; set; }
        Task Execute();
    }
}
