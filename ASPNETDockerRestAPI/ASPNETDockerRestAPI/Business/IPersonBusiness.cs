using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IPersonBusiness
    {
        PersonDto Create(PersonDto personDTO);
        PersonDto? FindById(long id);
        List<PersonDto> FindAll();
        PersonDto Update(PersonDto personDTO);
        PersonDto Disable(long id);
        void Delete(long id);
    }
}
