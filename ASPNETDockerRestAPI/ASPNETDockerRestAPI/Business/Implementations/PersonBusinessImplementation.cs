using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class PersonBusinessImplementation(IGenericRepository<PersonModel> personRepository, IPersonParser personParser) : IPersonBusiness
    {
        public PersonDto Create(PersonDto personDto)
        {
            var personModel = personParser.Parse(personDto);
            var createdPerson = personRepository.Create(personModel);

            return personParser.Parse(createdPerson);
        }

        public List<PersonDto> FindAll()
        {
            var books = personRepository.FindAll();

            return books.Select(personParser.Parse).ToList();
        }

        public PersonDto FindById(long id)
        {
            var book = personRepository.FindById(id);

            return personParser.Parse(book);
        }

        public PersonDto Update(PersonDto personDto)
        {
            var personModel = personParser.Parse(personDto);
            var updatedPerson = personRepository.Update(personModel);

            return personParser.Parse(updatedPerson);
        }

        public void Delete(long id) => personRepository.Delete(id);
    }
}
