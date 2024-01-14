using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Business
{
    public interface IPersonsBusiness
    {
        PersonModel Create(PersonModel person);
        PersonModel? FindById(long id);
        List<PersonModel> FindAll();
        PersonModel Update(PersonModel person);
        void Delete(long id);
    }
}
