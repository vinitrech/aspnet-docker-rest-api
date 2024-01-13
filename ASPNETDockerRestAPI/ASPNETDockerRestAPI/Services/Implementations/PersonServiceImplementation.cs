using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Services.Implementations
{
    public class PersonServiceImplementation(MySQLContext dbContext) : IPersonService
    {
        public Person Create(Person person)
        {
            dbContext.Add(person);
            dbContext.SaveChanges();

            return person;
        }

        public List<Person> FindAll()
        {
            return [.. dbContext.Persons];
        }

        public Person? FindById(long id) => dbContext.Find<Person>(id);

        public Person Update(Person person)
        {
            dbContext.Update(person);
            dbContext.SaveChanges();

            return person;
        }

        public void Delete(long id)
        {
            dbContext.Remove(FindById(id) ?? throw new Exception("Id not found"));
        }
    }
}
