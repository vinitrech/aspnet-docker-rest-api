using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class PersonsBusinessImplementation(IGenericRepository<PersonModel> personRepository) : IPersonsBusiness
    {
        public PersonModel Create(PersonModel person) => personRepository.Create(person);
        public List<PersonModel> FindAll() => personRepository.FindAll();
        public PersonModel? FindById(long id) => personRepository.FindById(id);
        public PersonModel Update(PersonModel person) => personRepository.Update(person);
        public void Delete(long id) => personRepository.Delete(id);
    }
}
