using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;

namespace Business.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IRepository<Connection> repository;
        private readonly IUnitofWork unitofWork;
        public ConnectionService(IRepository<Connection> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddConnection(Connection connection)
        {
            repository.Add(connection);
            unitofWork.saveChanges();
        }

        public void DeleteConnection(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public List<Connection> GetAllConnections()
        {
            var educations = repository.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            return educations;
        }

        public Connection GetConnectionById(Guid id)
        {
            var education = repository.GetById(id);
            return education;
        }

        public void UpdateConnection(Connection connection)
        {
            repository.Update(connection);
            unitofWork.saveChanges();
        }
        //public void UpdateEducationImage(string ImageUrl, int id)
        //{
        //    Education education = repository.GetById(id);
        //    education.MediaUrl = "https://localhost:44300/" + ImageUrl;
        //    repository.Update(education);
        //    unitofWork.saveChanges();
        //}
    }
}
