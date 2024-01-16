using ASPNETDockerRestAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETDockerRestAPI.Models
{
    [Table("books")]
    public class BookModel : BaseModel
    {
        [Column("author")]
        public string? Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public string? Title { get; set; }
    }
}