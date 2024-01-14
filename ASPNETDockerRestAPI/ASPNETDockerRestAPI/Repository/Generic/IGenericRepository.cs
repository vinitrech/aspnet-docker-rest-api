using ASPNETDockerRestAPI.Models.Base;

namespace ASPNETDockerRestAPI.Repository.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T? FindById(long id);
        List<T> FindAll();
        T Update(T entity);
        void Delete(long id);
    }
}
