using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class BooksRepositoryImplementation(MySQLContext dbContext) : IBooksRepository
    {
        public Book Create(Book book)
        {
            dbContext.Add(book);
            dbContext.SaveChanges();

            return book;
        }

        public List<Book> FindAll()
        {
            return [.. dbContext.Books];
        }

        public Book? FindById(long id) => dbContext.Find<Book>(id);

        public Book Update(Book book)
        {
            dbContext.Update(book);
            dbContext.SaveChanges();

            return book;
        }

        public void Delete(long id)
        {
            dbContext.Remove(FindById(id) ?? throw new Exception("Id not found"));
        }
    }
}
