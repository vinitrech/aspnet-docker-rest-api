using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Repository.Generic;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class BookBusinessImplementation(IGenericRepository<BookModel> bookRepository, IBookParser bookParser) : IBookBusiness
    {
        public BookDto Create(BookDto bookDto)
        {
            var bookModel = bookParser.Parse(bookDto);
            var createdBook = bookRepository.Create(bookModel);

            return bookParser.Parse(createdBook);
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

        public BookDto Update(BookDto bookDto)
        {
            var bookModel = bookParser.Parse(bookDto);
            var updatedBook = bookRepository.Update(bookModel);

            return bookParser.Parse(updatedBook);
        }

        public void Delete(long id) => bookRepository.Delete(id);
    }
}
