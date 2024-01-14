using System.Text.Json.Serialization;

namespace ASPNETDockerRestAPI.Dtos
{
    public class BookDto
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(1)]
        public long Id { get; set; }

        [JsonPropertyName("author")]
        [JsonPropertyOrder(3)]
        public string Author { get; set; }

        [JsonIgnore]
        public DateTime LaunchDate { get; set; }

        [JsonPropertyName("price")]
        [JsonPropertyOrder(2)]
        public decimal Price { get; set; }

        [JsonPropertyName("title")]
        [JsonPropertyOrder(4)]
        public string Title { get; set; }
    }
}
