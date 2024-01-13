using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Business
{
    public interface IPersonsBusiness
    {
        Person Create(Person person);
        Person? FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
