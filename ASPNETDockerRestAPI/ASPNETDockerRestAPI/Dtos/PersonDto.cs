﻿using System.Text.Json.Serialization;

namespace ASPNETDockerRestAPI.Dtos
{
    public class PersonDto
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(1)]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        [JsonPropertyOrder(3)]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [JsonPropertyOrder(4)]
        public string LastName { get; set; }

        [JsonPropertyName("address")]
        [JsonPropertyOrder(2)]
        public string Address { get; set; }

        [JsonIgnore]
        public string Gender { get; set; }
    }
}