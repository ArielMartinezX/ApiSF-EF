using Application.Interfaces;
using Models;

namespace Application.Commands
{
    public class GetFightersCommand : IGetFightersCommand
    {
        //inyecto el repo
        private readonly IFightersRepository _fightersRepository;

        public GetFightersCommand(IFightersRepository fightersRepository)
        {
            _fightersRepository = fightersRepository;
        }

        public ICollection<Fighter> Fighters { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }


        public async Task Execute()
        {
            try
            {
                Fighters = await _fightersRepository.GetFighters();
                Success = true;
                if (!Fighters.Any())
                {
                    Message = "No fighters found";
                }
            }
            catch (Exception ex)
            {
                Success = false;
                Message = ex.Message;
            }
        }
    }
}
