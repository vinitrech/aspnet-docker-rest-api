using ASPNETDockerRestAPI.Hypermedia;
using ASPNETDockerRestAPI.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace ASPNETDockerRestAPI.Dtos
{
    public class UserDto : ISupportsHypermedia
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(1)]
        public long Id { get; set; }

        [JsonPropertyName("user_name")]
        [JsonPropertyOrder(2)]
        public string? UserName { get; set; }

        [JsonPropertyName("full_name")]
        [JsonPropertyOrder(3)]
        public string? FullName { get; set; }

        [JsonPropertyName("password")]
        [JsonPropertyOrder(4)]
        public string? Password { get; set; }

        [JsonPropertyName("refresh_token")]
        [JsonPropertyOrder(5)]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expiry_time")]
        [JsonPropertyOrder(6)]
        public DateTime RefreshTokenExpiryTime { get; set; }

        [JsonPropertyOrder(7)]
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
