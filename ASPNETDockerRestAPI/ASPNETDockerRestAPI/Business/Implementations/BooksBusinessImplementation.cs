using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class BooksBusinessImplementation(IBooksRepository booksRepository) : IBooksBusiness
    {
        public Book Create(Book book) => booksRepository.Create(book);
        public List<Book> FindAll() => booksRepository.FindAll();
        public Book? FindById(long id) => booksRepository.FindById(id);
        public Book Update(Book book) => booksRepository.Update(book);
        public void Delete(long id) => booksRepository.Delete(id);
    }
}
