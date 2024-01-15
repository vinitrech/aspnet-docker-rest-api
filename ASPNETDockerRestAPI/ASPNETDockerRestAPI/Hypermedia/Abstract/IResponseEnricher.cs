using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNETDockerRestAPI.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
