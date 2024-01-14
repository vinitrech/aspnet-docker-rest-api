namespace ASPNETDockerRestAPI.Dtos
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
    }
}
