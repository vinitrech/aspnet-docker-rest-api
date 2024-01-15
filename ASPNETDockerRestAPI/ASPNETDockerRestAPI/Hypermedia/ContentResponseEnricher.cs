using ASPNETDockerRestAPI.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Concurrent;

namespace ASPNETDockerRestAPI.Hypermedia
{
    public abstract class ContentResponseEnricher<T>() : IResponseEnricher where T : ISupportsHypermedia
    {
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.CanEnrich(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }

            return false;
        }

        public async Task Enrich(ResultExecutingContext context)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(context);

            if (context.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> modelList)
                {
                    var bag = new ConcurrentBag<T>(modelList); // ConcurrentBag will store all items in a thread-safe manner, so that the items can be processed simultaneously by different threads.

                    Parallel.ForEach(bag, (element) => EnrichModel(element, urlHelper)); // Parallel.ForEach will discover and use all available threads. Each thread will process one iteration.
                }
            }

            await Task.FromResult<object>(null);
        }
    }
}
