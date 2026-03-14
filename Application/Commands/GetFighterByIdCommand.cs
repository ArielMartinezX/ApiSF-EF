using Application.Interfaces;
using Models;

namespace Application.Commands
{
    public class GetFighterByIdCommand : IGetFighterByIdCommand
    {
        private readonly IFightersRepository _fightersRepository;
        private string _id;

        public GetFighterByIdCommand(IFightersRepository fightersRepository)
        {
            _fightersRepository = fightersRepository;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public Fighter Fighter { get; set; }

        public IAsyncCommand Configure(string id)
        {
            _id = id;
            return this;
        }

        public async Task Execute()
        {
            try
            {
                Fighter = await _fightersRepository.GetFighterById(Guid.Parse(_id));
                if (Fighter != null)
                {
                    Success = true;
                    return;
                }
                Success = false;
                Message = "Fighter not found";
            }
            catch (Exception ex)
            {
                Success = false;
                Message = ex.Message;
            }
        }
    }
}
