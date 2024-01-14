using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Parsers
{
    public interface IPersonsParser
    {
        PersonDto Parse(PersonModel origin);
        PersonModel Parse(PersonDto origin);
        IEnumerable<PersonDto> Parse(IEnumerable<PersonModel> origin);
        IEnumerable<PersonModel> Parse(IEnumerable<PersonDto> origin);
    }
}
