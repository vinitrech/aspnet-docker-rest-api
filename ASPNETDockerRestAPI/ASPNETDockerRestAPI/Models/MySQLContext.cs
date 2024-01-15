using Microsoft.EntityFrameworkCore;
using System;

namespace ASPNETDockerRestAPI.Models
{
    public class MySqlContext : DbContext
    {
        protected MySqlContext()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
