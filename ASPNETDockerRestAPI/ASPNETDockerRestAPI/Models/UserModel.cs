﻿using ASPNETDockerRestAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETDockerRestAPI.Models
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        [Column("user_name")]
        public string? UserName { get; set; }

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
