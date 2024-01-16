using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.Generic.Implementations;
using Serilog;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class PersonRepositoryImplementation(MySqlContext mysqlContext) : GenericRepositoryImplementation<PersonModel>(mysqlContext), IPersonRepository
    {
        private MySqlContext DbContext { get; set; } = mysqlContext; // temporary fix, primary constructor messes up base parameters

        public PersonModel? Disable(long id)
        {
            var person = DbContext.Persons.SingleOrDefault(u => u.Id == id);

            if (person is null)
            {
                return null;
            }

            person.Enabled = false;

            try
            {
                DbContext.Update(person);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                Log.Error($"There was an error disabling person with id {id}");
                throw;
            }

            return person;
        }

        public List<PersonModel> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return [.. DbContext.Persons.Where(p => !string.IsNullOrWhiteSpace(p.FirstName) && p.FirstName.Contains(firstName) && !string.IsNullOrWhiteSpace(p.LastName) && p.LastName.Contains(lastName))];
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return [.. DbContext.Persons.Where(p => !string.IsNullOrWhiteSpace(p.FirstName) && p.FirstName.Contains(firstName))];
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return [.. DbContext.Persons.Where(p => !string.IsNullOrWhiteSpace(p.LastName) && p.LastName.Contains(lastName))];
            }

            return [];
        }
    }
}
