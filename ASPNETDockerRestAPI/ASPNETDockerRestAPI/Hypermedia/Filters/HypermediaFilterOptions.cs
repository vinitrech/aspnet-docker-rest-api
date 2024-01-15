using ASPNETDockerRestAPI.Hypermedia.Abstract;

namespace ASPNETDockerRestAPI.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnrichers { get; set; } = [];
    }
}
