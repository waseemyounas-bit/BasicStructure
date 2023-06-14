namespace DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(Guid id);
        IQueryable<T> GetAll();
        T GetById(Guid id);
        void Update(T entity);
    }
}
