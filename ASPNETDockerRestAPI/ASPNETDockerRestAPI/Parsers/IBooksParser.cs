using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Parsers
{
    public interface IBooksParser
    {
        BookDto Parse(BookModel origin);
        BookModel Parse(BookDto origin);
        IEnumerable<BookDto> Parse(IEnumerable<BookModel> origin);
        IEnumerable<BookModel> Parse(IEnumerable<BookDto> origin);
    }
}
