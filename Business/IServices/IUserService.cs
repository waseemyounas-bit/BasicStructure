using Data.Entities;

namespace Business.IServices
{
    public interface IUserService
    {
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUserById(Guid id);
        IQueryable<User> GetAllUser();
        void UpdateUserImage(string ImageUrl, Guid id);
    }
}
