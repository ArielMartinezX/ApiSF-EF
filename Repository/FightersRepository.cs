using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class FightersRepository : IFightersRepository
    {

        private readonly AppDbContext _appDbContext;

        public FightersRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Fighter>> GetFighters()
        {
            var fighters = await _appDbContext.Fighters
                .Where(x => x.IsAlive)
                .ToListAsync();
            return fighters;    
        }

        public async Task<Fighter> GetFighterById(Guid id)
        {
            var fighter = await _appDbContext.Fighters
                .FirstOrDefaultAsync(x => x.Id == id && x.IsAlive);
            return fighter;
        }

        public async Task<bool> DeleteFighter(Guid id)
        {
            var fighterToDelete = await GetFighterById(id);
            if (fighterToDelete != null)
            {
                fighterToDelete.IsAlive = false;

                await _appDbContext.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException($"Fighter with id {id} not found.");
        }

        public async Task<bool> UpdateFighter(Guid id, Fighter fighter)
        {
            var fighterToUpdate = await GetFighterById(id);
            if(fighterToUpdate != null)
            {
                fighterToUpdate.Name = fighter.Name;
                fighterToUpdate.Attack = fighter.Attack;
                fighterToUpdate.Defense = fighter.Defense;
                fighterToUpdate.Description = fighter.Description;
                fighterToUpdate.IsAlive = fighter.IsAlive;

                await _appDbContext.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException($"Fighter with id {id} not found.");
        }

        public async Task<Guid> InsertFighter(Fighter fighter)
        {
            fighter.Id = Guid.NewGuid();

            _appDbContext.Fighters.Add(fighter);
            await _appDbContext.SaveChangesAsync();

            return fighter.Id;
        }
    }
}
/*
 TAREA
 2) - TANTO EL GETALL COMO EL GETBYID VAN A TENER QUE FILTRAR POR ISALIVE --OK--
 3) - VALIDAR LOS METODOS GET CON TRY CATCH --creo que esta--
 4) - LOS METODOS QUE DEVUELVAN ROWS VALIDARLOS CON IF PERO LANZAR EXCEPCIONES -- no hay con rows, se maneja distinto--
 5) - TENER EN CUENTA QUE SI SE USAN TRY CATCH QUE LA EXCEPTION NO VIAJE LIBREMENTE PARA ARRIBA -- hecho del anterior--
      (REVISAR COMMANDS Y CONTROLLERS)
 6) - TENER DOS SOLUCIONES, UNA FUNCIONANDO CON DAPPER Y OTRA CON EF
 7) - RESOLVER EL VERSIONADO Y TENER LAS DOS APIS EN DOS REPOSITORIOS DISTINTOS EN GITHUB
 8) - IMPLEMENTAR GITHUB DESKTOP Y GITHUB EN EL VISUAL STUDIO
 */

/*
    CQRS investigar
*/

