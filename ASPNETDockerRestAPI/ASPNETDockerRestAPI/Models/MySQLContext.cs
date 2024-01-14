using Microsoft.EntityFrameworkCore;

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
    }
}
