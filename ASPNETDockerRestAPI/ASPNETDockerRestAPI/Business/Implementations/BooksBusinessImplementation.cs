using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class BooksBusinessImplementation(IGenericRepository<BookModel> bookRepository, IBooksParser bookParser) : IBooksBusiness
    {
        public BookDto Create(BookDto bookDTO)
        {
            bookRepository.Create(bookParser.Parse(bookDTO));

            return bookDTO;
        }

        public List<BookDto> FindAll()
        {
            var books = bookRepository.FindAll();

            return books.Select(bookParser.Parse).ToList();
        }

        public BookDto? FindById(long id)
        {
            var book = bookRepository.FindById(id);

            if (book is null)
            {
                return null;
            }

            return bookParser.Parse(book);
        }

        public BookDto Update(BookDto bookDTO)
        {
            bookRepository.Update(bookParser.Parse(bookDTO));

            return bookDTO;
        }

        public void Delete(long id) => bookRepository.Delete(id);
    }
}
