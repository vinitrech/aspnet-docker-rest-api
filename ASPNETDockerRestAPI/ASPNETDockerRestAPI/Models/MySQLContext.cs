using Microsoft.EntityFrameworkCore;

namespace ASPNETDockerRestAPI.Models
{
    public class MySQLContext : DbContext
    {
        protected MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
