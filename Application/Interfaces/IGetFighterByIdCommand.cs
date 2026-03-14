using Models;

namespace Application.Interfaces
{
    public interface IGetFighterByIdCommand : IAsyncCommand
    {
        //lo que necesito exponer publicamente
        public Fighter Fighter { get; set; }
        //patron de encadenamiento
        //metodo de configuracion donde los parametros necesarios son pasados para que el command pueda ejecutarse.
        //retorna IAsignCommand 
        IAsyncCommand Configure(string id);
    }
}
