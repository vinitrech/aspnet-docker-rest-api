using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IPersonBusiness
    {
        PersonDto Create(PersonDto personDto);
        PersonDto? FindById(long id);
        List<PersonDto?> FindByName(string firstName, string lastName);
        PagedSearchDto<PersonDto> FindAllPaged(string name, string sortDirection, int pageSize, int currentPage);
        PersonDto? Update(PersonDto personDto);
        PersonDto? Disable(long id);
        void Delete(long id);
    }
}
