using ASPNETDockerRestAPI.Models.Base;

namespace ASPNETDockerRestAPI.Repository
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        T? Create(T? entity);
        T? FindById(long id);
        List<T> FindAll();
        T? Update(T? entity);
        void Delete(long id);
        List<T> FindAllPaged(string query);
        int GetCount(string query);
    }
}
