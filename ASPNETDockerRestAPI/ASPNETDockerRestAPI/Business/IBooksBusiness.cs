using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IBooksBusiness
    {
        BookDto Create(BookDto bookDTO);
        BookDto? FindById(long id);
        List<BookDto> FindAll();
        BookDto Update(BookDto bookDTO);
        void Delete(long id);
    }
}
