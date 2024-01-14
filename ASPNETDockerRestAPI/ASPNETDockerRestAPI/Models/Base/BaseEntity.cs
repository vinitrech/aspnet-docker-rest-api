using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETDockerRestAPI.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}