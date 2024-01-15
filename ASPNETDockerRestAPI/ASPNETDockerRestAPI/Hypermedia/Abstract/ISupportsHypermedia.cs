namespace ASPNETDockerRestAPI.Hypermedia.Abstract
{
    public interface ISupportsHypermedia
    {
        public List<HypermediaLink> Links { get; set; }
    }
}
