using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Parsers.Implementations
{
    public class BookParserImplementation : IBookParser
    {
        public BookDto Parse(BookModel origin)
        {
            if (origin is null)
            {
                return null;
            }

            return new BookDto
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public BookModel Parse(BookDto origin)
        {
            if (origin is null)
            {
                return null;
            }

            return new BookModel
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public IEnumerable<BookDto> Parse(IEnumerable<BookModel> origin)
        {
            if (origin is null || !origin.Any())
            {
                return [];
            }

            return origin.Select(Parse);
        }

        public IEnumerable<BookModel> Parse(IEnumerable<BookDto> origin)
        {
            if (origin is null || !origin.Any())
            {
                return [];
            }

            return origin.Select(Parse);
        }
    }
}
