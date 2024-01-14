using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IBookBusiness
    {
        BookDto Create(BookDto bookDto);
        BookDto? FindById(long id);
        List<BookDto> FindAll();
        BookDto Update(BookDto bookDto);
        void Delete(long id);
    }
}
