
using Data.Entities;

namespace Business.IServices
{
    public interface IConnectionService
    {
        void AddConnection(Connection connection);
        void UpdateConnection(Connection connection);
        void DeleteConnection(Guid id);
        Connection GetConnectionById(Guid id);
        List<Connection> GetAllConnections();
    }
}
