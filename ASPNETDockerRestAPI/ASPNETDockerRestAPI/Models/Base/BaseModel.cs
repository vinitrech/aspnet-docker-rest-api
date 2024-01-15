using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETDockerRestAPI.Models.Base
{
    public class BaseModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
    }
}