

using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IUnitofWork unitofWork;
        public UserService(IRepository<User> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddUser(User user)
        {
            repository.Add(user);
            unitofWork.saveChanges();
        }

        public void DeleteUser(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public IQueryable<User> GetAllUser()
        {
            var users = repository.GetAll();
            return users;
        }

        public User GetUserById(Guid id)
        {
            var user = repository.GetById(id);
            return user;
        }

        public void UpdateUser(User user)
        {
            repository.Update(user);
            unitofWork.saveChanges();
        }

        public void UpdateUserImage(string ImageUrl, Guid id)
        {
            User user = repository.GetById(id);
            user.ProfileImage = "https://localhost:44346/" + ImageUrl;
            repository.Update(user);
            unitofWork.saveChanges();
        }
    }
}
