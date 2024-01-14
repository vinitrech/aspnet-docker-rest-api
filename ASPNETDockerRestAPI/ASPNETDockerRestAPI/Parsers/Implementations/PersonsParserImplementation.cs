using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Parsers.Implementations
{
    public class PersonsParserImplementation : IPersonsParser
    {
        public PersonDto Parse(PersonModel origin)
        {
            if (origin is null)
            {
                return null;
            }

            return new PersonDto
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender
            };
        }

        public PersonModel Parse(PersonDto origin)
        {
            if (origin is null)
            {
                return null;
            }

            return new PersonModel
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender
            };
        }

        public IEnumerable<PersonDto> Parse(IEnumerable<PersonModel> origin)
        {
            if (origin is null || !origin.Any())
            {
                return [];
            }

            return origin.Select(Parse).ToList();
        }

        public IEnumerable<PersonModel> Parse(IEnumerable<PersonDto> origin)
        {
            if (origin is null || !origin.Any())
            {
                return [];
            }

            return origin.Select(Parse).ToList();
        }
    }
}
