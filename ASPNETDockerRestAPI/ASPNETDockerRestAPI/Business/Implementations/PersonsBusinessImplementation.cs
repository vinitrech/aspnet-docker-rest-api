using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class PersonsBusinessImplementation(IGenericRepository<PersonModel> personRepository, IPersonsParser personParser) : IPersonsBusiness
    {
        public PersonDto Create(PersonDto personDTO)
        {
            personRepository.Create(personParser.Parse(personDTO));

            return personDTO;
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

        public PersonDto Update(PersonDto personDTO)
        {
            personRepository.Update(personParser.Parse(personDTO));

            return personDTO;
        }

        public void Delete(long id) => personRepository.Delete(id);
    }
}
