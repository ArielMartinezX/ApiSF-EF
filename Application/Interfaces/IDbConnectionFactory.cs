using System.Data;

namespace Application.Interfaces
{
    //clase que sirve para crear objetos, patron factory
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
