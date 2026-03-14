using Application.Interfaces;
using Models;

namespace Application.Commands
{
    public class InsertFighterCommand : IInsertFighterCommand
    {
        private readonly IFightersRepository _fightersRepository;
        private Fighter _newFighter;

        public InsertFighterCommand(IFightersRepository fightersRepository)
        {
            _fightersRepository = fightersRepository;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public Guid NewFighterId { get; private set; }

        public IAsyncCommand Configure(Fighter newFighter)
        {
            _newFighter = newFighter;
            return this;
        }

        public async Task Execute()
        {
            try
            {
                NewFighterId = await _fightersRepository.InsertFighter(_newFighter);
                Success = true;
                Message = "Fighter created succesfully";

            }
            catch (Exception)
            {
                Success = false;
                Message = "Fighter could not be created";
            }

        }
    }
}
