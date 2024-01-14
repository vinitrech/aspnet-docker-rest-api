using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IPersonsBusiness
    {
        PersonDto Create(PersonDto personDTO);
        PersonDto? FindById(long id);
        List<PersonDto> FindAll();
        PersonDto Update(PersonDto personDTO);
        void Delete(long id);
    }
}
