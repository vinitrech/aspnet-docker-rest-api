using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNETDockerRestAPI.Hypermedia.Filters
{
    public class HypermediaFilter(HypermediaFilterOptions hypermediaFilterOptions) : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult)
            {
                var enricher = hypermediaFilterOptions.ContentResponseEnrichers.Find(x => x.CanEnrich(context));

                if (enricher is null)
                {
                    return;
                }

                Task.FromResult(enricher.Enrich(context));
            }
        }
    }
}
