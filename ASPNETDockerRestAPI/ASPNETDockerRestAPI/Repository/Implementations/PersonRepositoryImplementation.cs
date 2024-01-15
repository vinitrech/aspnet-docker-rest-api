using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.Generic.Implementations;
using Serilog;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class PersonRepositoryImplementation(MySqlContext dbContext) : GenericRepositoryImplementation<PersonModel>(dbContext), IPersonRepository
    {
        public PersonModel Disable(long id)
        {
            var person = dbContext.Persons.SingleOrDefault(u => u.Id == id);

            if (person is null)
            {
                return null;
            }

            person.Enabled = false;

            try
            {
                dbContext.Update(person);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                Log.Error($"There was an error disabling person with id {id}");
                throw;
            }

            return person;
        }
    }
}
