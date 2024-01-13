using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindAll()
        {
            return [new(), new()];
        }

        public Person FindById(long id)
        {
            return new()
            {
                Address = "address",
                FirstName = "name",
                Gender = "male",
                Id = 1,
                LastName = "lastname"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
