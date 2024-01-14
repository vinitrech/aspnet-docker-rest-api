using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class PersonsBusinessImplementation(IGenericRepository<Person> personRepository) : IPersonsBusiness
    {
        public Person Create(Person person) => personRepository.Create(person);
        public List<Person> FindAll() => personRepository.FindAll();
        public Person? FindById(long id) => personRepository.FindById(id);
        public Person Update(Person person) => personRepository.Update(person);
        public void Delete(long id) => personRepository.Delete(id);
    }
}
