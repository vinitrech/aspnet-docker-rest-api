using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Repository;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class PersonBusinessImplementation(IPersonRepository personRepository, IPersonParser personParser) : IPersonBusiness
    {
        public PersonDto Create(PersonDto personDto)
        {
            var personModel = personParser.Parse(personDto);
            var createdPerson = personRepository.Create(personModel);

            return personParser.Parse(createdPerson)!;
        }

        public PagedSearchDto<PersonDto> FindAllPaged(string name, string sortDirection, int pageSize, int currentPage)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var query = @"select * from person p where 1 = 1";
            var countQuery = @"select count(*) from person p where 1 = 1";
            var offset = currentPage > 0 ? (currentPage - 1) * size : 0;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $" and p.first_name like '%{name}%'";
                countQuery += $" and p.first_name like '%{name}%'";
            }

            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            var persons = personRepository.FindAllPaged(query).ToList();
            var totalResults = personRepository.GetCount(countQuery);

            return new()
            {
                CurrentPage = currentPage,
                Items = personParser.Parse(persons).ToList(),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonDto? FindById(long id)
        {
            var person = personRepository.FindById(id);

            return personParser.Parse(person);
        }

        public List<PersonDto?> FindByName(string firstName, string lastName)
        {
            var personModels = personRepository.FindByName(firstName, lastName);

            return personModels.Select(personParser.Parse).Where(p => p is not null).ToList();
        }

        public PersonDto? Update(PersonDto personDto)
        {
            var personModel = personParser.Parse(personDto);
            var updatedPerson = personRepository.Update(personModel);

            return personParser.Parse(updatedPerson);
        }

        public PersonDto? Disable(long id)
        {
            var personModel = personRepository.Disable(id);

            return personParser.Parse(personModel);
        }

        public void Delete(long id) => personRepository.Delete(id);
    }
}
