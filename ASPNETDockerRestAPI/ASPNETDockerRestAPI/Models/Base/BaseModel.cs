﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETDockerRestAPI.Models.Base
{
    public class BaseModel
    {
        [Column("id")]
        public long Id { get; set; }
    }
}