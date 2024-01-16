using System.Text;

namespace ASPNETDockerRestAPI.Hypermedia
{
    public class HypermediaLink
    {
        private string? _href { get; set; }
        public string? Rel { get; set; }
        public string? Type { get; set; }
        public string? Action { get; set; }
        public string? Href
        {
            get
            {
                var _lock = new object();

                lock (_lock)
                {
                    var sb = new StringBuilder(_href);

                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set { _href = value; }
        }
    }
}
