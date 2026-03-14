using Application.Interfaces;
using Models;

namespace Application.Commands
{
    public class UpdateFighterCommand : IUpdateFighterCommand
    {
        private readonly IFightersRepository _fightersRepository;
        private string _id;
        private Fighter _fighterToUpdate;

        public UpdateFighterCommand(IFightersRepository fightersRepository)
        {
            _fightersRepository = fightersRepository;
        }

        public string Message { get; set; }
        public bool Success { get; set; }

        public IAsyncCommand Configure(string id, Fighter fighterToUpdate)
        {
            _id = id;
            _fighterToUpdate = fighterToUpdate;
            return this;
        }

        public async Task Execute()
        {
            try
            {
                await _fightersRepository.UpdateFighter(Guid.Parse(_id), _fighterToUpdate);
                Success = true;
                Message = "Fighter updated succesfully";
            }
            catch (Exception ex)
            {

                Success = false;
                Message = ex.Message;
            }
        }
    }
}
