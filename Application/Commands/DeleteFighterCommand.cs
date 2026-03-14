using Application.Interfaces;

namespace Application.Commands
{
    public class DeleteFighterCommand : IDeleteFighterCommand
    {
        private string _id { get; set; }
        private readonly IFightersRepository _fightersRepository;

        public DeleteFighterCommand(IFightersRepository fightersRepository)
        {
            _fightersRepository = fightersRepository;
        }

        public string Message { get; set; }
        public bool Success { get; set; }

        public IAsyncCommand Configure(string id)
        {
            _id = id;
            return this;
        }

        public async Task Execute()
        {
            try
            {
                await _fightersRepository.DeleteFighter(Guid.Parse(_id));
                Success = true;
                Message = "Fighter deleted";
            }
            catch (Exception ex)
            {
                Success = false;
                Message = ex.Message;
            }
        }
    }
}
