using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Repository
{
    public interface IPersonRepository : IGenericRepository<PersonModel>
    {
        PersonModel Disable(long id);
    }
}
